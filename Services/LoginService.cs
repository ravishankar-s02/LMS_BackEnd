using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LMS.Common;

namespace LMS.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString(Constants.databaseName));
            }
        }

        public LoginResponse Login(LoginRequest request, out int status, out string message)
        {
            status = 0;
            message = string.Empty;

            LoginResponse loginResponse = null;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    // Map model to parameters
                    parameters.Add("@EmpCode", request.empCode, DbType.String);
                    parameters.Add("@Password", request.password, DbType.String);

                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);
                    parameters.Add("@OutJSON", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);

                    // Call stored procedure
                    con.Execute(SPConstants.login, parameters, commandType: CommandType.StoredProcedure);

                    // Get output values
                    status = parameters.Get<short>("@Status");
                    message = parameters.Get<string>("@ErrorMessage");

                    // Deserialize JSON into LoginResponse
                    var outJson = parameters.Get<string>("@OutJSON");
                    if (!string.IsNullOrEmpty(outJson))
                    {
                        loginResponse = JsonConvert.DeserializeObject<LoginResponse>(outJson);
                        loginResponse.status = (byte)status;
                        loginResponse.errorMessage = message;
                    }
                    else
                    {
                        loginResponse = new LoginResponse
                        {
                            status = (byte)status,
                            errorMessage = message
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message;

                loginResponse = new LoginResponse
                {
                    status = (byte)status,
                    errorMessage = message
                };
            }

            return loginResponse;
        }
    }
}
