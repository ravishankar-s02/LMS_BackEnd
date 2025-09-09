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
    [Route("api/personal")]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly IMapper _mapper;

        public PersonalController(IPersonalService personalService, IMapper mapper)
        {
            _personalService = personalService;
            _mapper = mapper;
        }

        //1. To Get Employee Personal Details
        [HttpGet("employee/{empId}")]
        public ActionResult<List<EmployeeDetailsViewModel>> GetEmployeeDetails(string empId)
        {
            int status;
            string message;

            var employeeDetailsDMs = _personalService.GetEmployeeDetailsByEmpId(empId, out status, out message);
            
            List<EmployeeDetailsViewModel> result = _mapper.Map<List<EmployeeDetailsViewModel>>(employeeDetailsDMs);
            
            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        //2. To Get Employee Contact Details
        [HttpGet("contact/{empId}")]
        public ActionResult<List<ContactDetailsViewModel>> GetContactDetails(string empId)
        {
            int status;
            string message;

            var contactDetailsDMs = _personalService.GetContactDetailsByEmpId(empId, out status, out message);

            List<ContactDetailsViewModel> result = _mapper.Map<List<ContactDetailsViewModel>>(contactDetailsDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        //3. To Get Employee Team Details
        [HttpGet("team/{empId}")]
        public ActionResult<List<TeamDetailsViewModel>> GetTeamDetails(string empId)
        {
            int status;
            string message;

            var teamDetailsDMs = _personalService.GetTeamDetailsByEmpId(empId, out status, out message);

            List<TeamDetailsViewModel> result = _mapper.Map<List<TeamDetailsViewModel>>(teamDetailsDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }
    }
}
