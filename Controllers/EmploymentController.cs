using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces; 

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/employment")]
    public class EmploymentController : ControllerBase
    {
        private readonly IEmploymentService _employmentService;

        public EmploymentController(IEmploymentService employmentService)
        {
            _employmentService = employmentService;
        }

        // 1. To Get Job Details
        [HttpGet("job/{empId}")]
        public async Task<ActionResult<JobDetailsViewModel>> GetJobDetails(string empId)
        {
            var result = await _employmentService.GetJobDetailsByEmpIdAsync(empId);
            if (result == null)
                return NotFound(new { message = "Employee not found" });
            return Ok(result);
        }

        // 2. To Get Salary Details
        [HttpGet("salary/{empId}")]
        public async Task<ActionResult<SalaryDetailsViewModel>> GetSalaryDetails(string empId)
        {
            var result = await _employmentService.GetSalaryDetailsByEmpIdAsync(empId);
            if (result == null)
                return NotFound(new { message = "Employee not found" });
            return Ok(result);
        }
    }
}
