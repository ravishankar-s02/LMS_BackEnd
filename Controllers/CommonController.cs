using Microsoft.AspNetCore.Mvc;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _service;

        public CommonController(ICommonService service)
        {
            _service = service;
        }

        // 1. This is for Common Dropdowns
        [HttpGet("code-type/{type}")]
        public async Task<ActionResult<List<CommonViewModel>>> GetByType(string type)
        {
            var data = await _service.GetCommonByTypeAsync(type);
            return Ok(data);
        }

        // 2. This is for Team Dropdowns
        [HttpGet("team-type/{type}")]
        public async Task<ActionResult<List<TeamViewModel>>> GetByTeam(string type)
        {
            var data = await _service.GetCommonByTeamAsync(type);
            return Ok(data);
        }

        // 3. This is for Employee Name Dropdown
        [HttpGet("names")]
        public async Task<IActionResult> GetEmployeeNames()
        {
            var employees = await _service.GetEmployeesNameAsync();
            return Ok(employees);
        }
    }
}