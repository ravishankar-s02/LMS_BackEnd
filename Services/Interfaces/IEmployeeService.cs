using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeFullProfileViewModel> InsertFullEmployeeDetails(EmployeeFullProfileViewModel model); // Add New Employee
        //Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync(); // Get Employee Details
        List<EmployeeFullProfileViewModel> GetFullEmployeeProfile(out int status, out string message);
        Task<EmployeeFullProfileViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullProfileViewModel model); // Employee Updations
    }
}