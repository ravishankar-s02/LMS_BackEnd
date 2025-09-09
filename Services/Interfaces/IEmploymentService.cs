using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmploymentService
    {
        List<JobDetailsViewModel> GetJobDetailsByEmpId(string empCode, out int status, out string message);
        List<SalaryDetailsViewModel> GetSalaryDetailsByEmpId(string empCode, out int status, out string message);
    }
}