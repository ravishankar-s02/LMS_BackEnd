using System.Data;
using System.Threading.Tasks;
using Dapper;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LMS.Services
{
    public class GooseMenuService : IGooseMenuService
    {
        private readonly IConfiguration _configuration;
        public GooseMenuService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GooseMenuGroupedJsonModel> GetHierarchicalMenuAsync(string empCode)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_Code", empCode);
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<string>(
                "LMS_GetGooseMenu_Hierarchical",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (string.IsNullOrWhiteSpace(result))
            {
                return new GooseMenuGroupedJsonModel
                {
                    MAINMENU = new(),
                    BOTTOMMENU = new()
                };
            }
            return JsonConvert.DeserializeObject<GooseMenuGroupedJsonModel>(result);
        }
    }
}