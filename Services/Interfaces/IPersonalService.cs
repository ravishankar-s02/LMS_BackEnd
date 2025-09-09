using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IPersonalService
    {
        EmployeeDetailsViewModel GetEmployeeDetailsByEmpId(string empId, out int status, out string message); // Employee Personal Details
        ContactDetailsViewModel GetContactDetailsByEmpId(string empId, out int status, out string message); // Employee Contact Details
        TeamDetailsViewModel GetTeamDetailsByEmpId(string empId, out int status, out string message); // Employee Team Details
    }
}