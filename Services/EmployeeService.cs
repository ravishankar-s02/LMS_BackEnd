using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Models.ViewModels;
using LMS.Models.DataModels;
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
        
        public async Task<EmployeeFullProfileViewModel> InsertFullEmployeeDetails(EmployeeFullProfileViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<EmployeeFullProfileModel>(
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

            if (result == null)
                throw new Exception("No employee record returned from save");

            return _mapper.Map<EmployeeFullProfileViewModel>(result);
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


        public async Task<EmployeeFullProfileViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullProfileViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<EmployeeFullProfileModel>(
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
            return _mapper.Map<EmployeeFullProfileViewModel>(result);
        }
    }
}