using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using LMS.Common;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // 1. To Add New Employee
        [HttpPost("add-employee-details")]
        public IActionResult AddFullDetails([FromBody] EmployeeFullProfileViewModel model)
        {
            try
            {
                var result = _employeeService.InsertFullEmployeeDetails(model, out int status, out string message);
                return StatusCode(CommonUtility.HttpStatusCode(status), new { data = result, status, message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { data = new { }, status = -1, message = ex.Message });
            }
        }

        // 2. To Get Employee Details
        [HttpGet("view-employee-details")]
        public ActionResult<List<EmployeeFullProfileViewModel>> GetEmployeeProfile()
        {
            int status;
            string message;

            var empFullDetailsDMs = _employeeService.GetFullEmployeeProfile(out status, out message);

            List<EmployeeFullProfileViewModel> result = _mapper.Map<List<EmployeeFullProfileViewModel>>(empFullDetailsDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        // 3. To Update Employee Details
        [Route("update-employee-details")]
        [HttpPut]
        public ActionResult<EmployeeFullProfileViewModel> UpdateEmpDetails([FromBody] EmployeeFullProfileViewModel updateEmpDetailsVM)
        {
            var updateEmpDetailsDM = _employeeService.UpdateEmpDetails(updateEmpDetailsVM, out int status, out string message);
            EmployeeFullProfileViewModel result = _mapper.Map<EmployeeFullProfileViewModel>(updateEmpDetailsDM);

            return StatusCode(CommonUtility.HttpStatusCode(status), new { data = result, status, message });
        }
    }
}