using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Lấy token từ header Authorization
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Giải mã token
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            // Kiểm tra thời gian hết hạn của token
            var expirationTime = decodedToken.ValidTo;
            var now = DateTime.UtcNow;

            if (now > expirationTime)
            {
                // Token đã hết hạn
                return BadRequest("Token has expired.");
            }
            else
            {
                // Token vẫn còn hiệu lực
                return Ok("Token is valid.");
            }
        }
    }
}
