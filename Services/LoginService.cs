using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LMS.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@EmpCode", request.empCode);
            parameters.Add("@Password", request.password);
            parameters.Add("@Status", dbType: DbType.Byte, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 5000, direction: ParameterDirection.Output);
            parameters.Add("@OutJSON", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("SS_EmployeeLogin_SP", parameters, commandType: CommandType.StoredProcedure);

            var status = parameters.Get<byte>("@Status");
            var errorMessage = parameters.Get<string>("@ErrorMessage");
            var outJson = parameters.Get<string>("@OutJSON");

            LoginUserDetails userDetails = null;

            if (!string.IsNullOrEmpty(outJson))
            {
                userDetails = JsonConvert.DeserializeObject<LoginUserDetails>(outJson);
            }

            return new LoginResponse
            {
                status = status,
                errorMessage = errorMessage,
                result = userDetails
            };
        }
    }
}