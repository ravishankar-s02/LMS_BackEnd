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
                    model.EmployeeCode,
                    model.FirstName,
                    model.LastName,
                    model.DateOfBirth,
                    model.Gender,
                    model.MaritalStatus,
                    model.Nationality,
                    model.PhoneNumber,
                    model.AlternateNumber,
                    model.Email,
                    model.StreetAddress,
                    model.City,
                    model.State,
                    model.ZipCode,
                    model.Country,
                    model.Designation,
                    model.TeamHRHead,
                    model.ProjectManager,
                    model.TeamLead,
                    model.JobTitle,
                    model.EmploymentStatus,
                    model.JoinDate,
                    model.Skillset,
                    model.PayGrade,
                    model.Currency,
                    model.BasicSalary,
                    model.PayFrequency,
                    model.InsertedUser
                },
                commandType: CommandType.StoredProcedure);

            if (result.Status == 1)
                return true;

            throw new Exception(result.Message); // Or return result.Message back to controller
        }

        public async Task<EmployeeFullProfileViewModel> GetEmployeeFullProfileAsync(string empCode)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new { EmpCode = empCode };
            var result = await connection.QueryFirstOrDefaultAsync<EmployeeFullProfileViewModel>(
                "SS_GetEmployeeFullProfile_SP", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullDetailsPUTViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "SS_UpdateFullEmployeeDetails_SP",
                new
                {
                    model.EmployeeCode,
                    model.FirstName,
                    model.LastName,
                    model.DateOfBirth,
                    model.Gender,
                    model.MaritalStatus,
                    model.Nationality,
                    model.PhoneNumber,
                    model.AlternateNumber,
                    model.Email,
                    model.StreetAddress,
                    model.City,
                    model.State,
                    model.ZipCode,
                    model.Country,
                    model.Designation,
                    model.TeamHRHead,
                    model.ProjectManager,
                    model.TeamLead,
                    model.JobTitle,
                    model.EmploymentStatus,
                    model.JoinDate,
                    model.Skillset,
                    model.PayGrade,
                    model.Currency,
                    model.BasicSalary,
                    model.PayFrequency,
                    model.UpdatedUser
                },
                commandType: CommandType.StoredProcedure);

            if (result.Status == 1)
                return true;

            throw new Exception(result.Message);
        }

    }
}