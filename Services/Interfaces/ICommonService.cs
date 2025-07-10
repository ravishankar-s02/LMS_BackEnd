using LMS.Models.ViewModels;

public interface ICommonService
{
    Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType);
}
