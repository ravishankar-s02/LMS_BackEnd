using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmploymentService
    {
        JobDetailsViewModel GetJobDetailsByEmpId(string empCode, out int status, out string message);
        SalaryDetailsViewModel GetSalaryDetailsByEmpId(string empCode, out int status, out string message);
    }
}