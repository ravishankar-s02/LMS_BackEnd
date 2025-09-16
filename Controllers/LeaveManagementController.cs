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
        [Route("leave/apply")]
        [HttpPost]
        public IActionResult ApplyLeave([FromBody] LeaveApplicationViewModel leaveApplication)
        {
            long leavePk = _leaveManagementService.ApplyLeave(leaveApplication, out int status, out string message);
            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = new {}, status, message });
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
        [Route("update-leave")]
        [HttpPut]
        public ActionResult<LeaveUpdateViewModel> UpdateLeave([FromBody] LeaveUpdateViewModel updateLeaveViewModel)
        {
            var updateLeaveDM = _leaveManagementService.UpdateLeave(updateLeaveViewModel, out int status, out string message);
            LeaveUpdateViewModel result = _mapper.Map<LeaveUpdateViewModel>(updateLeaveDM);

            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = result, status, message });
        }

        // 4. To Perform Delete Operation
        [Route("delete-leave")]
        [HttpPut]
        public ActionResult<LeaveDeleteViewModel> DeleteLeave([FromBody] LeaveDeleteViewModel deleteLeaveViewModel)
        {
            var deleteLeaveDM = _leaveManagementService.DeleteLeave(deleteLeaveViewModel, out int status, out string message);
            LeaveDeleteViewModel result = _mapper.Map<LeaveDeleteViewModel>(deleteLeaveDM);

            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = result, status, message });
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
        [Route("leave-action/update")]
        [HttpPost]
        public IActionResult UpdateLeaveRequest([FromBody] LeaveActionRequestViewModel leaveRequestUpdate)
        {
            var updatedList = _leaveManagementService.UpdateLeaveRequest(leaveRequestUpdate, out int status, out string message);
            
            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = updatedList, status, message });
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