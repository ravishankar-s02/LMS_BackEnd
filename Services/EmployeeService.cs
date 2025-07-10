using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Models.DataModels; 
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;

public class EmployeeService : IEmployeeService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public EmployeeService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<EmployeeDetailsViewModel?> GetEmployeeDetailsByEmpIdAsync(string empId)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var parameters = new DynamicParameters();
        parameters.Add("@SS_Emp_ID", empId);

        var result = await connection.QueryFirstOrDefaultAsync<EmployeeDetailsModel>(
            "sp_GetEmployeeDetailsByEmpId",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        if (result == null)
            return null;

        // Map to ViewModel
        var viewModel = _mapper.Map<EmployeeDetailsViewModel>(result);
        return viewModel;
    }
}
