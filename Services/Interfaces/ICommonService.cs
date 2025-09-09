using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface ICommonService
    {
        //Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType); // Common Dropdowns
        List<CommonViewModel> GetCommonByType(string type, out int status, out string message);
        //Task<List<TeamViewModel>> GetCommonByTeamAsync(string codeType);   // Team Dropdowns
        List<TeamViewModel> GetCommonByTeam(string type, out int status, out string message);
        //Task<List<EmployeeNameViewModel>> GetEmployeesNameAsync(string empCode); // Employee Name Dropdown
        List<EmployeeNameViewModel> GetCommonByEmployeesName(string empCode, out int status, out string message);
    }
}
