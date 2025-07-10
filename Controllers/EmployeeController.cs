using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces; 

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{empId}")]
    public async Task<ActionResult<EmployeeDetailsViewModel>> GetEmployeeDetails(string empId)
    {
        var result = await _employeeService.GetEmployeeDetailsByEmpIdAsync(empId);

        if (result == null)
            return NotFound(new { message = "Employee not found" });

        return Ok(result);
    }
}
