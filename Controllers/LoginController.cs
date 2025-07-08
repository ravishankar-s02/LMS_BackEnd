using Microsoft.AspNetCore.Mvc;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;

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

        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _loginService.LoginAsync(request);
            if (response.Status == 1)
                return Ok(response);
            else
                return Unauthorized(response);
        }
    }
}
