using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("add-employee-details")]
        public async Task<IActionResult> AddFullDetails([FromBody] EmployeeFullDetailsViewModel model)
        {
            try
            {
                var success = await _employeeService.InsertFullEmployeeDetails(model);
                return Ok(new { message = "Employee details inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("view-employee-details")]
        public async Task<IActionResult> GetEmployeeProfile()
        {
            var history = await _employeeService.GetEmployeeFullProfileAsync();
            return Ok(history);
        }

        [HttpPut("update-employee-details")]
        public async Task<IActionResult> UpdateEmployeeProfile([FromBody] EmployeeFullDetailsPUTViewModel model)
        {
            try
            {
                var result = await _employeeService.UpdateFullEmployeeDetailsPUTAsync(model);
                return result ? Ok("Profile updated successfully") : BadRequest("Update failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}