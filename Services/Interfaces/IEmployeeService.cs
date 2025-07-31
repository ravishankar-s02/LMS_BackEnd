// using System.Threading.Tasks;
// using LMS.Models.ViewModels;

// namespace LMS.Services.Interfaces
// {
//     public interface IEmployeeService
//     {
//         Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model);
//         Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync();
//         Task<bool> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model);

//     }
// }

using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model);
        Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync();
        Task<EmployeeFullDetailsViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model);
    }
}
