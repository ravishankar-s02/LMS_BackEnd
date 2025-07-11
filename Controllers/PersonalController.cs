using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces; 

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/personal")]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;

        public PersonalController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpGet("employee/{empId}")]
        public async Task<ActionResult<EmployeeDetailsViewModel>> GetEmployeeDetails(string empId)
        {
            var result = await _personalService.GetEmployeeDetailsByEmpIdAsync(empId);

            if (result == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(result);
        }

        [HttpGet("contact/{empId}")]
        public async Task<ActionResult<ContactDetailsViewModel>> GetContactDetails(string empId)
        {
            var result = await _personalService.GetContactDetailsByEmpIdAsync(empId);

            if (result == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(result);
        }

        [HttpGet("team/{empId}")]
        public async Task<ActionResult<TeamDetailsViewModel>> GetTeamDetails(string empId)
        {
            var result = await _personalService.GetTeamDetailsByEmpIdAsync(empId);

            if (result == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(result);
        }
    }
}
