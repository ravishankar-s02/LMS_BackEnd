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
    [Route("api/employment")]
    public class EmploymentController : ControllerBase
    {
        private readonly IEmploymentService _employmentService;
        private readonly IMapper _mapper;

        public EmploymentController(IEmploymentService employmentService, IMapper mapper)
        {
            _employmentService = employmentService;
            _mapper = mapper;
        }

        // 1. To Get Job Details
        [HttpGet("job/{empId}")]
        public ActionResult<List<JobDetailsViewModel>> GetJobDetails(string empId)
        {
            int status;
            string message;

            var jobDetailsDMs = _employmentService.GetJobDetailsByEmpId(empId, out status, out message);

            List<JobDetailsViewModel> result = _mapper.Map<List<JobDetailsViewModel>>(jobDetailsDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }

        // 2. To Get Salary Details
        [HttpGet("salary/{empId}")]
        public ActionResult<List<SalaryDetailsViewModel>> GetSalaryDetails(string empId)
        {
            int status;
            string message;

            var salaryDetailsDMs = _employmentService.GetSalaryDetailsByEmpId(empId, out status, out message);

            List<SalaryDetailsViewModel> result = _mapper.Map<List<SalaryDetailsViewModel>>(salaryDetailsDMs);

            return StatusCode(
                CommonUtility.HttpStatusCode(status),
                new { data = result, status = status, message = message }
            );
        }
    }
}
