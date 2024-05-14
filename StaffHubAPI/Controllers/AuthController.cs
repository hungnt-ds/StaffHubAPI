using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DTOs;
using StaffHubAPI.Services.Interfaces;
using Claim = System.Security.Claims.Claim;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IAuthenticationService authenticationService, IUserService userService)
        {
            _config = config;
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterRequestDTO request)
        {
            _authenticationService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User()
            {
                Name = request.UserName,
                ContractSalary = request.ContractSalary,
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DaysOff = 0,
                RoleId = 1
            };
            _userService.CreateUser(newUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginRequestDTO login)
        {

            var user = _userService.GetUser(login.UserName);
            if (login.UserName != user.UserName)
            {
                return BadRequest("User not found.");
            }

            if (!_authenticationService.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = _authenticationService.CreateToken(user);
            var refreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(user, refreshToken);

            return Ok(token);

        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var userName = _authenticationService.GetUserName();
            var user = _userService.GetUser(userName);
            var refreshToken = Request.Cookies["refreshToken"];
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired");
            }

            string token = _authenticationService.CreateToken(user);
            var newRefreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(user, newRefreshToken);

            return Ok(token);
        }

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        RefToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        ExpierTime = DateTime.Now.AddDays(1),
        //        Created = DateTime.Now,
        //    };

        //    return refreshToken;
        //}

        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.ExpierTime
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.RefToken, cookieOptions);

        //    //user.RefreshToken = newRefreshToken.RefToken;
        //    //user.TokenCreated = newRefreshToken.Created;
        //    //user.TokenExpires = newRefreshToken.ExpierTime;
        //}

        //private string CreateToken(User user)
        //{

        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        //new Claim(ClaimTypes.Role, "Admin"),
        //        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}
        ///////////////////////////

        //[HttpPost("login")]
        //public IActionResult Login(UserLoginRequestDTO login)
        //{
        //    IActionResult response = Unauthorized();
        //    var user = _userService.AuthenticateUser(login);

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    var tokenString = GenerateJWTToken(user);
        //    response = Ok(new { token = tokenString });
        //    return response;
        //    //var user = AuthenticateUser(login);

        //    //if (user != null)
        //    //{
        //    //    var tokenString = GenerateJWTToken(user);
        //    //    response = Ok(new { token = tokenString });
        //    //}

        //    //return response;
        //}

        //private User AuthenticateUser(User login)
        //{
        //    var users = _userService.GetUsers();

        //    var user = users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);
        //    return user;
        //}

        private string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: new[] {
                    new System.Security.Claims.Claim(ClaimTypes.Name, userInfo.UserName),
                    new System.Security.Claims.Claim(ClaimTypes.Role, userInfo.RoleId.ToString()),
                },
                expires: DateTime.Now.AddSeconds(20),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpPost("refresh-token")]
        //public IActionResult RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];

        //    if (refreshToken == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var newToken = _authenticationService.RefreshToken(refreshToken);

        //    if (newToken == null)
        //    {
        //        return Unauthorized();
        //    }

        //    return Ok(new { token = newToken });
        //}
    }
}
