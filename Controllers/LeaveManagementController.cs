using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using LMS.Models.DataModels;

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

        // 1. To Apply Leave
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveApplicationViewModel model)
        { 
            // To Convert totalHours from string to decimal?
            decimal? parsedTotalHours = decimal.TryParse(model.totalHours, out var th) ? th : (decimal?)null;

            // Prepare data model to pass to service
            var dataModel = new LeaveApplicationModel
            {
                EmpCode = model.empCode,
                LeaveType = model.leaveType,
                FromDate = model.fromDate,
                ToDate = model.toDate,
                FromTime = model.fromTime,
                ToTime = model.toTime,
                TotalHours = parsedTotalHours,
                Duration = model.duration,
                Reason = model.reason
            };

            var (status, message) = await _leaveManagementService.ApplyLeaveAsync(dataModel);
            if (status == 1)
                return Ok(new { message });
            return BadRequest(new { message });
        }

        // 2. To Get My Leave History
        [HttpGet("my-history/{empCode}")]
        public async Task<IActionResult> GetMyLeaveHistory(string empCode)
        {
            var history = await _leaveManagementService.GetMyLeaveHistoryAsync(empCode);
            return Ok(history);
        }

        // 3. To Get Users Leave History
        [HttpGet("users-history/{empCode}")]
        public async Task<IActionResult> GetUsersLeaveHistory(string empCode)
        {
            var history = await _leaveManagementService.GetUsersLeaveHistoryAsync(empCode);
            return Ok(history);
        }

        // 4. To Perform Update Leave Operation
        [HttpPut("update-leave")]
        public async Task<IActionResult> UpdateLeave([FromBody] LeaveUpdateModel model)
        {
            var result = await _leaveManagementService.UpdateLeaveAsync(model);
            if (result.Status == 1)
                return Ok(new { result.Status, result.Message });
            else
                return BadRequest(new { result.Status, result.Message });
        }

        // 4. To Perform Delete Operation
        [HttpPut("delete-leave")]
        public async Task<IActionResult> DeleteLeave([FromBody] LeaveDeleteModel model)
        {
            var result = await _leaveManagementService.DeleteLeaveAsync(model);
            if (result.Status == 1)
                return Ok(new { result.Status, result.Message });
            else
                return BadRequest(new { result.Status, result.Message });
        }

        // 5. To Get Leave Requests
        [HttpGet("leave-request/{empCode}")]
        public async Task<IActionResult> GetLeaveAction([FromRoute] string empCode)
        {
            if (string.IsNullOrWhiteSpace(empCode))
                return BadRequest("empCode is required.");

            var history = await _leaveManagementService.GetLeaveActionAsync(empCode);
            return Ok(history);
        }

        // 6. To Approve or Cancel the Leave
        [HttpPost("leave-action/update")]
        public async Task<IActionResult> UpdateLeaveAction([FromBody] LeaveActionRequestModel model)
        {
            var updatedList = await _leaveManagementService.UpdateLeaveActionAsync(model);
            return Ok(updatedList);
        }

        // 7. To Get Leave Summary
        [HttpGet("my-leave-summary/{empCode}")]
        public async Task<IActionResult> GetMyLeaveSummary(string empCode)
        {
            var history = await _leaveManagementService.GetMyLeaveSummaryAsync(empCode);
            return Ok(history);
        }
    }
}