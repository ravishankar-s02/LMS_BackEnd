using Dapper;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;
using LMS.Services.Interfaces;

public class CommonService : ICommonService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public CommonService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<List<CommonViewModel>> GetCommonByTypeAsync(string codeType)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var result = await connection.QueryAsync<CommonModel>(
            "SS_GetCommonByType_SP",
            new { CodeType = codeType },
            commandType: CommandType.StoredProcedure
        );

        return _mapper.Map<List<CommonViewModel>>(result);
    }

    public async Task<List<TeamViewModel>> GetCommonByTeamAsync(string codeType)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var result = await connection.QueryAsync<TeamModel>(
            "SS_GetTeam_SP",
            new { CodeType = codeType },
            commandType: CommandType.StoredProcedure
        );

        return _mapper.Map<List<TeamViewModel>>(result);
    }

    public async Task<List<TimeViewModel>> GetCommonByTimeAsync()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var result = await connection.QueryAsync<TimeModel>(
            "SS_GetTimeDropdown_SP",
            commandType: CommandType.StoredProcedure
        );

        return _mapper.Map<List<TimeViewModel>>(result);
    }
}
