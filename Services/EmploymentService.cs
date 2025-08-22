using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
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

        public async Task<JobDetailsViewModel?> GetJobDetailsByEmpIdAsync(string empCode)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_Code", empCode);

            var result = await connection.QueryFirstOrDefaultAsync<JobDetailsModel>(
                "SS_JobDetails_SP",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return result == null ? null : _mapper.Map<JobDetailsViewModel>(result);
        }

        public async Task<SalaryDetailsViewModel?> GetSalaryDetailsByEmpIdAsync(string empCode)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_Code", empCode);

            var result = await connection.QueryFirstOrDefaultAsync<SalaryDetailsModel>(
                "SS_SalaryDetails_SP",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return result == null ? null : _mapper.Map<SalaryDetailsViewModel>(result);
        }
    }
}