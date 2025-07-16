using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveManagementController : ControllerBase
    {
        private readonly ILeaveManagementService _leaveManagementService;

        public LeaveManagementController(ILeaveManagementService leaveManagementService)
        {
            _leaveManagementService = leaveManagementService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveApplicationViewModel model)
        {
            var (status, message) = await _leaveManagementService.ApplyLeaveAsync(model);
            if (status == 1)
                return Ok(new { message });

            return BadRequest(new { message });
        }

        [HttpGet("my-history")]
        public async Task<IActionResult> GetMyLeaveHistory(string empCode)
        {
            var history = await _leaveManagementService.GetMyLeaveHistoryAsync(empCode);
            return Ok(history);
        }

        [HttpGet("users-history")]
        public async Task<IActionResult> GetUsersLeaveHistory()
        {
            var history = await _leaveManagementService.GetUsersLeaveHistoryAsync();
            return Ok(history);
        }
    }
}