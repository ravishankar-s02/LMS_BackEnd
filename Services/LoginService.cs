using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

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
            parameters.Add("@UserName", request.UserName);
            parameters.Add("@Password", request.Password);
            parameters.Add("@Status", dbType: DbType.Byte, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 5000, direction: ParameterDirection.Output);
            parameters.Add("@OutJSON", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("sp_EmployeeLogin", parameters, commandType: CommandType.StoredProcedure);

            return new LoginResponse
            {
                Status = parameters.Get<byte>("@Status"),
                ErrorMessage = parameters.Get<string>("@ErrorMessage"),
                Result = parameters.Get<string>("@OutJSON")
            };
        }
    }
}
