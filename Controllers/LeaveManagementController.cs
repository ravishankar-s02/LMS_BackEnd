using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using LMS.Models.DataModels;
using LMS.Common;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveManagementController : ControllerBase
    {
        private readonly ILeaveManagementService _leaveManagementService;
        private readonly IMapper _mapper;

        public LeaveManagementController(ILeaveManagementService leaveManagementService, IMapper mapper)
        {
            _leaveManagementService = leaveManagementService;
            _mapper = mapper;
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
        public ActionResult<List<MyLeaveHistoryModel>> GetMyLeaveHistory(string empCode)
        {
            int status;
            string message;

            var myLeaveHistoryDMs = _leaveManagementService.GetMyLeaveHistoryByEmpId(empCode, out status, out message);

            List<MyLeaveHistoryViewModel> result = _mapper.Map<List<MyLeaveHistoryViewModel>>(myLeaveHistoryDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        // 3. To Get Users Leave History
        [HttpGet("users-history/{empCode}")]
        public ActionResult<List<UsersLeaveHistoryViewModel>> GetUsersLeaveHistory(string empCode)
        {
            int status;
            string message;

            var usersLeaveHistoryDMs = _leaveManagementService.GetUsersLeaveHistoryByEmpId(empCode, out status, out message);

            List<UsersLeaveHistoryViewModel> result = _mapper.Map<List<UsersLeaveHistoryViewModel>>(usersLeaveHistoryDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
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
        public ActionResult<List<LeaveActionViewModel>> GetLeaveRequest(string empCode)
        {
            int status;
            string message;

            var leaveRequestDMs = _leaveManagementService.GetLeaveRequestByEmpId(empCode, out status, out message);

            List<LeaveActionViewModel> result = _mapper.Map<List<LeaveActionViewModel>>(leaveRequestDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
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
        public ActionResult<List<MyLeaveSummaryViewModel>> GetLeaveSummary(string empCode)
        {
            int status;
            string message;

            var leaveSummaryDMs = _leaveManagementService.GetLeaveSummaryByEmpId(empCode, out status, out message);

            List<MyLeaveSummaryViewModel> result = _mapper.Map<List<MyLeaveSummaryViewModel>>(leaveSummaryDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }
    }
}