using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IPersonalService
    {
        Task<EmployeeDetailsViewModel?> GetEmployeeDetailsByEmpIdAsync(string empId);
        Task<ContactDetailsViewModel?> GetContactDetailsByEmpIdAsync(string empId);
        Task<TeamDetailsViewModel?> GetTeamDetailsByEmpIdAsync(string empId);
    }
}

