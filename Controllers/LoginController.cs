using Microsoft.AspNetCore.Mvc;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using LMS.Common;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // 1. To Login
        [Route("")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var loginList = _loginService.Login(request, out int status, out string message);
            
            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = loginList, status, message });
        }
    }
}
