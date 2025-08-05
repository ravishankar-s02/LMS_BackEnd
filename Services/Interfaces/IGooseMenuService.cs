using LMS.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Services.Interfaces
{
    public interface IGooseMenuService
    {
        Task<GooseMenuGroupedJsonModel> GetHierarchicalMenuAsync(string empCode);
    }
}
