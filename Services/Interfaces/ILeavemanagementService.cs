using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;

namespace LMS.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationModel model); // Apply Leave
        //Task<IEnumerable<MyLeaveHistoryViewModel>> GetMyLeaveHistoryAsync(string empCode); // My Leave
        List<MyLeaveHistoryViewModel> GetMyLeaveHistoryByEmpId(string empCode, out int status, out string message);
        //Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync(string empCode); // Users Leave
        List<UsersLeaveHistoryViewModel> GetUsersLeaveHistoryByEmpId(string empCode, out int status, out string message);
        //Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateViewModel model); // Update Leave
        LeaveUpdateModel UpdateLeave(LeaveUpdateViewModel updateLeaveViewModel, out int status, out string message);
        Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model); // Delete Leave
        //Task<IEnumerable<LeaveActionViewModel>> GetLeaveActionAsync(string empCode); // Leave Request
        List<LeaveActionViewModel> GetLeaveRequestByEmpId(string empCode, out int status, out string message);
        Task<IEnumerable<LeaveActionViewModel>> UpdateLeaveActionAsync(LeaveActionRequestModel model); // Approve or Cancel
        //Task<IEnumerable<MyLeaveSummaryViewModel>> GetMyLeaveSummaryAsync(string empCode); // Leave Summary
        List<MyLeaveSummaryViewModel> GetLeaveSummaryByEmpId(string empCode, out int status, out string message);
    }
}