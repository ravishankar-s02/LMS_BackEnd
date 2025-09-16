using LMS.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Services.Interfaces
{
    public interface IGooseMenuService
    {
        //Task<GooseMenuGroupedJsonModel> GetHierarchicalMenuAsync(string empCode); // Team Filterations [hierarchy]
        GooseMenuGroupedJsonModel GetHierarchicalMenu(string empCode, out int status, out string message);
    }
}