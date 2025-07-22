using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LMS.Services.Interfaces
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
                    model.employeeCode,
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
                    model.joinDate,
                    model.skillset,
                    model.payGrade,
                    model.currency,
                    model.basicSalary,
                    model.payFrequency,
                    model.insertedUser
                },
                commandType: CommandType.StoredProcedure);

            if (result.Status == 1)
                return true;

            throw new Exception(result.Message); // Or return result.Message back to controller
        }

        public async Task<IEnumerable<EmployeeFullProfileViewModel>> GetEmployeeFullProfileAsync()
        {
            var result = await _db.QueryAsync<EmployeeFullProfileViewModel>(
                "SS_GetEmployeeFullProfile_SP",
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "SS_UpdateFullEmployeeDetails_SP",
                new
                {
                    model.employeeCode,
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
                    model.joinDate,
                    model.skillset,
                    model.payGrade,
                    model.currency,
                    model.basicSalary,
                    model.payFrequency,
                    model.updatedUser
                },
                commandType: CommandType.StoredProcedure);

            if (result.Status == 1)
                return true;

            throw new Exception(result.Message);
        }

    }
}