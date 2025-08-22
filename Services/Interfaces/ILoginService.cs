using LMS.Models.ViewModels;

namespace LMS.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request); // Login
    }
}