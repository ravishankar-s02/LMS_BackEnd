using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;

namespace LMS.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationModel model); // Apply Leave
        Task<IEnumerable<MyLeaveHistoryViewModel>> GetMyLeaveHistoryAsync(string empCode); // My Leave
        Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync(string empCode); // Users Leave
        Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateModel model); // Update Leave
        Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model); // Delete Leave
        Task<IEnumerable<LeaveActionViewModel>> GetLeaveActionAsync(string empCode); // Leave Request
        Task<IEnumerable<LeaveActionViewModel>> UpdateLeaveActionAsync(LeaveActionRequestModel model); // Approve or Cancel
        Task<IEnumerable<MyLeaveSummaryViewModel>> GetMyLeaveSummaryAsync(string empCode); // Leave Summary
    }
}