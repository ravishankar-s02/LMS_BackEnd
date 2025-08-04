using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces; 

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GooseMenuController : ControllerBase
    {
        private readonly IGooseMenuService _gooseMenuService;

        public GooseMenuController(IGooseMenuService gooseMenuService)
        {
            _gooseMenuService = gooseMenuService;
        }

        [HttpGet("menu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _gooseMenuService.GetGooseMenuAsync();
            return Ok(result);
        }
    }
}