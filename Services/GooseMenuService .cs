using System.Data;
using System.Threading.Tasks;
using Dapper;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LMS.Common;
using AutoMapper;

namespace LMS.Services
{
    public class GooseMenuService : IGooseMenuService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public GooseMenuService(IConfiguration configuration, IMapper mapper)
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
        
        public GooseMenuGroupedJsonModel GetHierarchicalMenu(string empCode, out int status, out string message)
        {
            GooseMenuGroupedJsonModel gooseMenu = null;
            status = 0;
            message = string.Empty;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    parameters.Add("@SS_Emp_Code", empCode, DbType.String, ParameterDirection.Input, 50);
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                    parameters.Add("@ErrMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parameters.Add("@OutJSON", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);

                    con.Execute(SPConstants.gooseMenu, parameters, commandType: CommandType.StoredProcedure);

                    status = parameters.Get<short>("@Status");
                    message = parameters.Get<string>("@ErrMsg");

                    var jsonResult = parameters.Get<string>("@OutJSON");
                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        gooseMenu = JsonConvert.DeserializeObject<GooseMenuGroupedJsonModel>(jsonResult);
                    }
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message;
            }

            return gooseMenu;
        }
    }
}