using System.Data;
using System.Threading.Tasks;
using Dapper;
using LMS.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class GooseMenuService : IGooseMenuService
{
    private readonly IConfiguration _configuration;

    public GooseMenuService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetGooseMenuJsonAsync()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await connection.QueryFirstOrDefaultAsync<string>(
            "SS_GetGooseMenu_Hierarchical_JSON_SP",
            commandType: CommandType.StoredProcedure
        );

        return result ?? "{}"; // Return empty JSON object if null
    }
}
