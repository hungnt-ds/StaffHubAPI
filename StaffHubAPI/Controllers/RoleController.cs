using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _sevice;

        public RoleController(IRoleService sevice)
        {
            _sevice = sevice;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Skip header row if present

                    var interns = new List<Role>();
                    foreach (var row in rows)
                    {
                        var intern = new Role
                        {
                            RoleName = row.Cell(1).GetString()
                        };
                        interns.Add(intern);
                    }

                    await _sevice.CreateRole(interns);
                }
            }

            return Ok("File uploaded and data saved successfully.");
        }
    }
}
