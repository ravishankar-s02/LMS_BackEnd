using System.Threading.Tasks;
using LMS.Models.ViewModels;

public interface IEmployeeService
{
    Task<EmployeeDetailsViewModel?> GetEmployeeDetailsByEmpIdAsync(string empId);
}
