using Microsoft.AspNetCore.Mvc;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces; 

[ApiController]
[Route("api/common")]
public class CommonController : ControllerBase
{
    private readonly ICommonService _service;

    public CommonController(ICommonService service)
    {
        _service = service;
    }

    [HttpGet("code-type/{type}")]
    public async Task<ActionResult<List<CommonViewModel>>> GetByType(string type)
    {
        var data = await _service.GetCommonByTypeAsync(type);
        return Ok(data);
    }

    [HttpGet("team-type/{type}")]
    public async Task<ActionResult<List<TeamViewModel>>> GetByTeam(string type)
    {
        var data = await _service.GetCommonByTeamAsync(type);
        return Ok(data);
    }
}
