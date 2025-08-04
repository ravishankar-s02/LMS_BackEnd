using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;

namespace LMS.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationModel model);
        Task<IEnumerable<MyLeaveHistoryViewModel>> GetMyLeaveHistoryAsync(string empCode);
        Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync();
        Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateModel model);
        Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model);
        Task<IEnumerable<LeaveActionViewModel>> GetLeaveActionAsync();
        Task<(int Status, string Message)> UpdateLeaveStatusAsync(long leavePK, string action);
        Task<LeaveActionViewModel> GetLeaveApplicationByIdAsync(long leavePK);
    }
}