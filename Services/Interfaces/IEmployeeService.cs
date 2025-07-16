using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model);
        Task<EmployeeFullProfileViewModel> GetEmployeeFullProfileAsync(string empCode);
        Task<bool> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model);

    }
}