using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Common;
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

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString(Constants.databaseName));
            }
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

        public List<EmployeeFullProfileViewModel> GetFullEmployeeProfile(out int status, out string message)
        {
            var empFullDetailsVMs = new List<EmployeeFullProfileViewModel>();
            status = 0;
            message = string.Empty;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5000);

                    var result = con.Query<EmployeeFullProfileModel>(
                        SPConstants.empFullDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    empFullDetailsVMs = result.Select(r => _mapper.Map<EmployeeFullProfileViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return empFullDetailsVMs;
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