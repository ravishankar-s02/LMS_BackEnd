using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EmployeeService(IConfiguration config, IMapper mapper)
        {
            _configuration = config;
            _mapper = mapper;
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "LMS_InsertFullEmployeeDetails",
                new
                {
                    model.empPk,
                    model.empCode,
                    model.firstName,
                    model.lastName,
                    model.dateOfBirth,
                    model.gender,
                    model.maritalStatus,
                    model.nationality,
                    model.phoneNumber,
                    model.alternateNumber,
                    model.email,
                    model.streetAddress,
                    model.city,
                    model.state,
                    model.zipCode,
                    model.country,
                    model.designation,
                    model.teamHRHead,
                    model.projectManager,
                    model.teamLead,
                    model.jobTitle,
                    model.employmentStatus,
                    model.joinedDate,
                    model.skillset,
                    model.payGrade,
                    model.currency,
                    model.basicSalary,
                    model.payFrequency
                },
                commandType: CommandType.StoredProcedure);

            if (result.Status == 1)
                return true;
            throw new Exception(result.Message);
        }

        public async Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryAsync<EmployeeFullProfileModel>(
                "LMS_GetEmployeeFullDetails",
                commandType: CommandType.StoredProcedure
            );

            return _mapper.Map<IEnumerable<EmployeeFullProfileViewModel>>(result);
        }


        public async Task<EmployeeFullDetailsViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<EmployeeFullDetailsViewModel>(
                "LMS_UpdateFullEmployeeDetails",
                new
                {   
                    model.empPk,
                    model.empCode,
                    model.firstName,
                    model.lastName,
                    model.dateOfBirth,
                    model.gender,
                    model.maritalStatus,
                    model.nationality,
                    model.phoneNumber,
                    model.alternateNumber,
                    model.email,
                    model.streetAddress,
                    model.city,
                    model.state,
                    model.zipCode,
                    model.country,
                    model.designation,
                    model.teamHRHead,
                    model.projectManager,
                    model.teamLead,
                    model.jobTitle,
                    model.employmentStatus,
                    model.joinedDate,
                    model.skillset,
                    model.payGrade,
                    model.currency,
                    model.basicSalary,
                    model.payFrequency,
                    model.empStatus
                },
                commandType: CommandType.StoredProcedure);

            if (result == null)
                throw new Exception("No employee record returned from update");
            return result;
        }
    }
}