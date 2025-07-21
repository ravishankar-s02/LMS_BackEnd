using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationViewModel model);
        Task<IEnumerable<MyLeaveHistoryViewModel>> GetMyLeaveHistoryAsync(string empCode);
        Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync();
        Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateModel model);
        Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model);
    }
}