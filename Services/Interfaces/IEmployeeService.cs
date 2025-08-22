using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model); // Add New Employee
        Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync(); // Get Employee Details
        Task<EmployeeFullDetailsViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model); // Employee Updations
    }
}