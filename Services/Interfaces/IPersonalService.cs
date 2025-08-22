using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IPersonalService
    {
        Task<EmployeeDetailsViewModel?> GetEmployeeDetailsByEmpIdAsync(string empId); // Employee Personal Details
        Task<ContactDetailsViewModel?> GetContactDetailsByEmpIdAsync(string empId); // Employee Contact Details
        Task<TeamDetailsViewModel?> GetTeamDetailsByEmpIdAsync(string empId); // Employee Team Details
    }
}