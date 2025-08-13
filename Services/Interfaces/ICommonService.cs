using LMS.Models.ViewModels;

public interface ICommonService
{
    Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType);
    Task<List<TeamViewModel>> GetCommonByTeamAsync(string codeType);
    Task<IEnumerable<EmployeeNameViewModel>> GetEmployeesNameAsync();
}
