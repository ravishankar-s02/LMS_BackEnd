using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;

        public EmployeeService(IConfiguration config)
        {
            _configuration = config;
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> InsertFullEmployeeDetails(EmployeeFullDetailsViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "SS_InsertFullEmployeeDetails_SP",
                new
                {
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
            var result = await _db.QueryAsync<EmployeeFullProfileViewModel>(
                "SS_GetEmployeeFullProfile_SP",
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<EmployeeFullDetailsViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<EmployeeFullDetailsViewModel>(
                "SS_UpdateFullEmployeeDetails_SP",
                new
                {   
                    model.empFk,
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
