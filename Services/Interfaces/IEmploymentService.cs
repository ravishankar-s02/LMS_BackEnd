using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmploymentService
    {
        Task<JobDetailsViewModel?> GetJobDetailsByEmpIdAsync(string empId); // Job Details
        Task<SalaryDetailsViewModel?> GetSalaryDetailsByEmpIdAsync(string empId); // Salary Details
    }
}