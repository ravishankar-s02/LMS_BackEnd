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
    public class PersonalService : IPersonalService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PersonalService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsViewModel?> GetEmployeeDetailsByEmpIdAsync(string empId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_ID", empId);

            var result = await connection.QueryFirstOrDefaultAsync<EmployeeDetailsModel>(
                "sp_GetEmployeeDetailsByEmpId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result == null ? null : _mapper.Map<EmployeeDetailsViewModel>(result);
        }

        public async Task<ContactDetailsViewModel?> GetContactDetailsByEmpIdAsync(string empId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_ID", empId);

            var result = await connection.QueryFirstOrDefaultAsync<ContactDetailsModel>(
                "sp_GetContactDetailsByEmpId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result == null ? null : _mapper.Map<ContactDetailsViewModel>(result);
        }

        public async Task<TeamDetailsViewModel?> GetTeamDetailsByEmpIdAsync(string empId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_ID", empId);

            var result = await connection.QueryFirstOrDefaultAsync<TeamDetailsModel>(
                "sp_GetTeamDetailsByEmpId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result == null ? null : _mapper.Map<TeamDetailsViewModel>(result);
        }
    }
}