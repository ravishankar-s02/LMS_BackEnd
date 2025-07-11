using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmploymentService
    {
        Task<JobDetailsViewModel?> GetJobDetailsByEmpIdAsync(string empId);
        Task<SalaryDetailsViewModel?> GetSalaryDetailsByEmpIdAsync(string empId);
    }
}

