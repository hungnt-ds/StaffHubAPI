using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.DataAccess.DTOs;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IActualSalaryService _actualSalaryService;

        public AuthController(
            IAuthenticationService authenticationService,
            IUserService userService,
            IMapper mapper,
            IActualSalaryService actualSalaryService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _mapper = mapper;
            _actualSalaryService = actualSalaryService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRespondDTO>> Register(UserRegisterRequestDTO request)
        {
            var existingUser = _userService.GetUserByUserName(request.UserName);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Username already exists." });
            }

            _authenticationService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = _mapper.Map<UserRegisterRequestDTO, User>(request);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.DaysOff = 0;

            _userService.CreateUser(newUser);

            var actualSalary = new ActualSalary
            {
                DaysOff = 0,
                SalaryAfterDeductions = newUser.ContractSalary,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                UserId = newUser.UserId
            };
            _actualSalaryService.CreateActualSalary(actualSalary);

            var userResponse = _mapper.Map<User, UserRespondDTO>(newUser);

            return Ok(userResponse);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginRequestDTO login)
        {

            var user = _userService.GetUserByUserName(login.UserName);
            if (login.UserName != user.UserName)
            {
                return BadRequest("User not found.");
            }

            if (!user.Status)
            {
                return BadRequest("User is inactive and cannot login.");
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
            var user = _userService.GetUserByUserName(userName);
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
    }
}
