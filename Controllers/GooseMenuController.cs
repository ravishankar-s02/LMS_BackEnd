using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LMS.Services.Interfaces;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using System.Threading.Tasks;
using LMS.Common;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GooseMenuController : ControllerBase
    {
        private readonly IGooseMenuService _menuService;
        private readonly IMapper _mapper;

        public GooseMenuController(IGooseMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        // 1. To Perform Hierarchy Operation
        [HttpGet("hierarchical-menu/{empCode}")]
        public ActionResult<GooseMenuGroupedJsonModel> GetHierarchicalMenu(string empCode)
        {
            int status;
            string message;

            var gooseMenuDMs = _menuService.GetHierarchicalMenu(empCode, out status, out message);

            GooseMenuGroupedJsonModel result = _mapper.Map<GooseMenuGroupedJsonModel>(gooseMenuDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }
    }
}
