using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model);
    }
}