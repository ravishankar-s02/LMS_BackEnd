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
    public class EmploymentService : IEmploymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EmploymentService(IConfiguration configuration, IMapper mapper)
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

        // public async Task<JobDetailsViewModel?> GetJobDetailsByEmpIdAsync(string empCode)
        // {
        //     using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //     var parameters = new DynamicParameters();
        //     parameters.Add("@SS_Emp_Code", empCode);

        //     var result = await connection.QueryFirstOrDefaultAsync<JobDetailsModel>(
        //         "LMS_JobDetails",
        //         parameters,
        //         commandType: CommandType.StoredProcedure
        //     );
        //     return result == null ? null : _mapper.Map<JobDetailsViewModel>(result);
        // }
        public List<JobDetailsViewModel> GetJobDetailsByEmpId(string empCode, out int status, out string message)
        {
            var jobDetailsVMs = new List<JobDetailsViewModel>();
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

                    var result = con.Query<JobDetailsModel>(
                        SPConstants.jobDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    jobDetailsVMs = result.Select(r => _mapper.Map<JobDetailsViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return jobDetailsVMs;
        }

        public List<SalaryDetailsViewModel> GetSalaryDetailsByEmpId(string empCode, out int status, out string message)
        {
            var salaryDetailsVMs = new List<SalaryDetailsViewModel>();
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

                    var result = con.Query<SalaryDetailsModel>(
                        SPConstants.salaryDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    salaryDetailsVMs = result.Select(r => _mapper.Map<SalaryDetailsViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return salaryDetailsVMs;
        }
    }
}