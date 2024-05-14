using Microsoft.IdentityModel.Tokens;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace StaffHubAPI.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _refreshTokenKey;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _refreshTokenKey = configuration["Jwt:RefreshTokenKey"];
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                RefToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpierTime = DateTime.Now.AddDays(1),
                Created = DateTime.Now,
            };

            return refreshToken;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                //new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpierTime
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken.RefToken, cookieOptions);

            user.RefreshToken = newRefreshToken.RefToken;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.ExpierTime;
            _unitOfWork.UserObj.UpdateUser(user);
        }

        public string RefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                var tempo = _httpContextAccessor.HttpContext.User;
                result = tempo.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
