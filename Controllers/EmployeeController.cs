using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("add-full-details")]
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
    }
}