using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Common;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LMS.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PersonalService(IConfiguration configuration, IMapper mapper)
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

        public EmployeeDetailsViewModel GetEmployeeDetailsByEmpId(string empCode, out int status, out string message)
        {
            var employeeDetailsVMs = new EmployeeDetailsViewModel();
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
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                    var result = con.Query<EmployeeDetailsModel>(
                        SPConstants.employeeDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // ✅ Map Model → ViewModel
                    employeeDetailsVMs = result.Select(r => _mapper.Map<EmployeeDetailsViewModel>(r)).FirstOrDefault();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return employeeDetailsVMs;
        }

        public ContactDetailsViewModel GetContactDetailsByEmpId(string empCode, out int status, out string message)
        {
            var contactDetailsVMs = new ContactDetailsViewModel();
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
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                    var result = con.Query<ContactDetailsModel>(
                        SPConstants.contactDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // ✅ Map Model → ViewModel
                    contactDetailsVMs = result.Select(r => _mapper.Map<ContactDetailsViewModel>(r)).FirstOrDefault();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return contactDetailsVMs;
        }

        public TeamDetailsViewModel GetTeamDetailsByEmpId(string empCode, out int status, out string message)
        {
            var teamDetailsVMs = new TeamDetailsViewModel();
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
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                    var result = con.Query<TeamDetailsModel>(
                        SPConstants.teamDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // ✅ Map Model → ViewModel
                    teamDetailsVMs = result.Select(r => _mapper.Map<TeamDetailsViewModel>(r)).FirstOrDefault();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return teamDetailsVMs;
        }
    }
}