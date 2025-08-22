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

        // 1. To Add New Employee
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

        // 2. To Get Employee Details
        [HttpGet("view-employee-details")]
        public async Task<IActionResult> GetEmployeeProfile()
        {
            var history = await _employeeService.GetEmployeeFullProfileAsync();
            return Ok(history);
        }

        // 3. To Update Employee Details
        [HttpPut("update-employee-details")]
        public async Task<IActionResult> UpdateEmployeeProfile([FromBody] EmployeeFullDetailsPUTViewModel model)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateFullEmployeeDetailsPUTAsync(model);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}