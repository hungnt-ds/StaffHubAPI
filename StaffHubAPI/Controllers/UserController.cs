using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.UnitOfWork;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("all"), Authorize(Roles = "1")]
        public IActionResult GetAllCars()
        {
            // Lấy danh sách các xe
            var cars = new List<User>()
            {
                new User { UserId = 1, Name = "Toyota", UserName = "Camry", ContractSalary = 30000 },
                new User { UserId = 2, Name = "Toyota", UserName = "Camry", ContractSalary = 30000 },
                new User { UserId = 3, Name = "Toyota", UserName = "Camry", ContractSalary = 30000 },
            };

            return Ok(cars);
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetCar(int id)
        {
            // Lấy thông tin xe theo id
            var user = new User { UserId = 3, Name = "Toyota", UserName = "Camry", ContractSalary = 30000 };

            return Ok(user);
        }
    }
}
