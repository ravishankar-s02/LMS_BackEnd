using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using LMS.Common;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _service;
        private readonly IMapper _mapper;

        public CommonController(ICommonService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // 1. This is for Common Dropdowns
        // [HttpGet("code-type/{type}")]
        // public async Task<ActionResult<List<CommonViewModel>>> GetByType(string type)
        // {
        //     var data = await _service.GetCommonByTypeAsync(type);
        //     return Ok(data);
        // }
        [HttpGet("code-type/{type}")]
        public ActionResult<List<CommonViewModel>> GetByType(string type)
        {
            int status;
            string message;

            var commonDropdownDMs = _service.GetCommonByType(type, out status, out message);

            List<CommonViewModel> result = _mapper.Map<List<CommonViewModel>>(commonDropdownDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        // 2. This is for Team Dropdowns
        [HttpGet("team-type/{type}")]
        public ActionResult<List<TeamViewModel>> GetByTeam(string type)
        {
            int status;
            string message;

            var teamDropdownDMs = _service.GetCommonByTeam(type, out status, out message);

            List<TeamViewModel> result = _mapper.Map<List<TeamViewModel>>(teamDropdownDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        // 3. This is for Employee Name Dropdown
        [HttpGet("names/{empCode}")]
        public ActionResult<List<EmployeeNameViewModel>> GetByEmployeesName(string empCode)
        {
            int status;
            string message;

            var empDropdownDMs = _service.GetCommonByEmployeesName(empCode, out status, out message);

            List<EmployeeNameViewModel> result = _mapper.Map<List<EmployeeNameViewModel>>(empDropdownDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

    }
}