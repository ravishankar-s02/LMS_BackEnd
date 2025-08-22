using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface ICommonService
    {
        Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType); // Common Dropdowns
        Task<List<TeamViewModel>> GetCommonByTeamAsync(string codeType); // Team Dropdowns
        Task<IEnumerable<EmployeeNameViewModel>> GetEmployeesNameAsync(); // Employee Name Dropdown
    }
}