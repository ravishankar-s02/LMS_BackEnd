using Microsoft.AspNetCore.Mvc;
using LMS.Services.Interfaces;
using LMS.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GooseMenuController : ControllerBase
    {
        private readonly IGooseMenuService _menuService;

        public GooseMenuController(IGooseMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("hierarchical-menu/{empCode}")]
        public async Task<ActionResult<GooseMenuGroupedJsonModel>> GetHierarchicalMenu(string empCode)
        {
            var result = await _menuService.GetHierarchicalMenuAsync(empCode);

            if (result == null)
                return NotFound("No menu found for the employee.");

            return Ok(result);
        }
    }
}
