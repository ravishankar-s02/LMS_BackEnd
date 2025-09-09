using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using AutoMapper;
using LMS.Common;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

public class CommonService : ICommonService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public CommonService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IDbConnection Connection
    {
        get
        {
            return new SqlConnection(_configuration.GetConnectionString(Constants.databaseName));
        }
    }

    // public async Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType)
    // {
    //     using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    //     var result = await connection.QueryAsync<CommonModel>(
    //         "LMS_GetCommonByType",
    //         new { CodeType = codeType },
    //         commandType: CommandType.StoredProcedure
    //     );
    //     return _mapper.Map<List<CommonViewModel>>(result);
    // }

    public List<CommonViewModel> GetCommonByType(string type, out int status, out string message)
    {
        var commonDropdownVMs = new List<CommonViewModel>();
        status = 0;
        message = string.Empty;

        try
        {
            using (IDbConnection con = Connection)
            {
                con.Open();
                var parameters = new DynamicParameters();

                parameters.Add("@CodeType", type, DbType.String, ParameterDirection.Input, 50);
                parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                var result = con.Query<CommonModel>(
                    SPConstants.commonDropdowns,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // ✅ Map Model → ViewModel
                commonDropdownVMs = result.Select(r => _mapper.Map<CommonViewModel>(r)).ToList();

                status = parameters.Get<Int16>("@Status");
                message = parameters.Get<string>("@ErrMsg");
            }
        }
        catch (Exception)
        {
            status = 5;
            message = "An Exception Thrown";
        }

        return commonDropdownVMs;
    }

    public List<TeamViewModel> GetCommonByTeam(string type, out int status, out string message)
    {
        var teamDropdownVMs = new List<TeamViewModel>();
        status = 0;
        message = string.Empty;

        try
        {
            using (IDbConnection con = Connection)
            {
                con.Open();
                var parameters = new DynamicParameters();

                parameters.Add("@CodeType", type, DbType.String, ParameterDirection.Input, 50);
                parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                var result = con.Query<TeamModel>(
                    SPConstants.teamDropdowns,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // ✅ Map Model → ViewModel
                teamDropdownVMs = result.Select(r => _mapper.Map<TeamViewModel>(r)).ToList();

                status = parameters.Get<Int16>("@Status");
                message = parameters.Get<string>("@ErrMsg");
            }
        }
        catch (Exception)
        {
            status = 5;
            message = "An Exception Thrown";
        }

        return teamDropdownVMs;
    }

    public List<EmployeeNameViewModel> GetCommonByEmployeesName(string empCode, out int status, out string message)
    {
        var empDropdownVMs = new List<EmployeeNameViewModel>();
        status = 0;
        message = string.Empty;

        try
        {
            using (IDbConnection con = Connection)
            {
                con.Open();
                var parameters = new DynamicParameters();

                parameters.Add("@SS_Emp_code", empCode, DbType.String, ParameterDirection.Input, 50);
                parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                var result = con.Query<EmployeeNameModel>(
                    SPConstants.empDropdowns,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // ✅ Map Model → ViewModel
                empDropdownVMs = result.Select(r => _mapper.Map<EmployeeNameViewModel>(r)).ToList();

                status = parameters.Get<Int16>("@Status");
                message = parameters.Get<string>("@ErrMsg");
            }
        }
        catch (Exception)
        {
            status = 5;
            message = "An Exception Thrown";
        }

        return empDropdownVMs;
    }
}