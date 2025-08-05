using System.Threading.Tasks;
using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface IGooseMenuService
    {
        Task<string> GetGooseMenuJsonAsync();
    }
}
