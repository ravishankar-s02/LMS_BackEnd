using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

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

            // Get hashed password from DB
            var getPwdQuery = "SELECT Login_Password FROM SS_Registered_Login WHERE Login_Email_ID = @EmpCode AND Login_Status = 'Active'";
            var hashedPwd = await connection.QueryFirstOrDefaultAsync<string>(getPwdQuery, new { request.empCode });

            if (hashedPwd == null || !BCrypt.Net.BCrypt.Verify(request.password, hashedPwd))
            {
                return new LoginResponse
                {
                    status = 0,
                    errorMessage = "Invalid credentials",
                    result = null
                };
            }

            // Continue with SP for additional info
            var parameters = new DynamicParameters();
            parameters.Add("@EmpCode", request.empCode);
            parameters.Add("@Password", hashedPwd); // or pass empty, not used
            parameters.Add("@Status", dbType: DbType.Byte, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 5000, direction: ParameterDirection.Output);
            parameters.Add("@OutJSON", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("SS_EmployeeLogin_SP", parameters, commandType: CommandType.StoredProcedure);

            return new LoginResponse
            {
                status = parameters.Get<byte>("@Status"),
                errorMessage = parameters.Get<string>("@ErrorMessage"),
                result = parameters.Get<string>("@OutJSON")
            };
        }
    }
}
