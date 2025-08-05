using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class GooseMenuController : ControllerBase
{
    private readonly IGooseMenuService _gooseMenuService;

    public GooseMenuController(IGooseMenuService gooseMenuService)
    {
        _gooseMenuService = gooseMenuService;
    }

    [HttpGet("hierarchical")]
    public async Task<IActionResult> GetHierarchicalMenu()
    {
        var json = await _gooseMenuService.GetGooseMenuJsonAsync();
        return Content(json, "application/json"); // Direct JSON response
    }
}
