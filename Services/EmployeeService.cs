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
        
        public EmployeeFullProfileViewModel InsertFullEmployeeDetails(EmployeeFullProfileViewModel model, out int status, out string message)
        {
            status = 0;
            message = string.Empty;
            EmployeeFullProfileViewModel employee = null;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    // Input params
                    parameters.Add("@EmpPk", model.empPk, DbType.String); // though SP does not use it much
                    parameters.Add("@EmpCode", model.empCode, DbType.String);
                    parameters.Add("@FirstName", model.firstName, DbType.String);
                    parameters.Add("@LastName", model.lastName, DbType.String);
                    parameters.Add("@DateOfBirth", model.dateOfBirth, DbType.Date);
                    parameters.Add("@Gender", model.gender, DbType.String);
                    parameters.Add("@MaritalStatus", model.maritalStatus, DbType.String);
                    parameters.Add("@Nationality", model.nationality, DbType.String);
                    parameters.Add("@PhoneNumber", model.phoneNumber, DbType.String);
                    parameters.Add("@AlternateNumber", model.alternateNumber, DbType.String);
                    parameters.Add("@Email", model.email, DbType.String);
                    parameters.Add("@StreetAddress", model.streetAddress, DbType.String);
                    parameters.Add("@City", model.city, DbType.String);
                    parameters.Add("@State", model.state, DbType.String);
                    parameters.Add("@ZipCode", model.zipCode, DbType.String);
                    parameters.Add("@Country", model.country, DbType.String);
                    parameters.Add("@Designation", model.designation, DbType.String);
                    parameters.Add("@TeamHRHead", model.teamHRHead, DbType.String);
                    parameters.Add("@ProjectManager", model.projectManager, DbType.String);
                    parameters.Add("@TeamLead", model.teamLead, DbType.String);
                    parameters.Add("@JobTitle", model.jobTitle, DbType.String);
                    parameters.Add("@EmploymentStatus", model.employmentStatus, DbType.String);
                    parameters.Add("@JoinedDate", model.joinedDate, DbType.Date);
                    parameters.Add("@Skillset", model.skillset, DbType.String);
                    parameters.Add("@PayGrade", model.payGrade, DbType.String);
                    parameters.Add("@Currency", model.currency, DbType.String);
                    parameters.Add("@BasicSalary", model.basicSalary, DbType.Decimal);
                    parameters.Add("@PayFrequency", model.payFrequency, DbType.String);

                    // Output params
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                    parameters.Add("@ErrMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                    // Execute & get employee profile from SELECT at end of SP
                    var result = con.QueryFirstOrDefault<EmployeeFullProfileModel>(
                        "LMS_InsertFullEmployeeDetails",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    if (result != null)
                        employee = _mapper.Map<EmployeeFullProfileViewModel>(result);

                    status = parameters.Get<short>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message;
            }

            return employee;
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


        // public async Task<EmployeeFullProfileViewModel> UpdateFullEmployeeDetailsPUTAsync(EmployeeFullProfileViewModel model)
        // {
        //     var result = await _db.QueryFirstOrDefaultAsync<EmployeeFullProfileModel>(
        //         "LMS_UpdateFullEmployeeDetails",
        //         new
        //         {   
        //             model.empPk,
        //             model.empCode,
        //             model.firstName,
        //             model.lastName,
        //             model.dateOfBirth,
        //             model.gender,
        //             model.maritalStatus,
        //             model.nationality,
        //             model.phoneNumber,
        //             model.alternateNumber,
        //             model.email,
        //             model.streetAddress,
        //             model.city,
        //             model.state,
        //             model.zipCode,
        //             model.country,
        //             model.designation,
        //             model.teamHRHead,
        //             model.projectManager,
        //             model.teamLead,
        //             model.jobTitle,
        //             model.employmentStatus,
        //             model.joinedDate,
        //             model.skillset,
        //             model.payGrade,
        //             model.currency,
        //             model.basicSalary,
        //             model.payFrequency,
        //             model.empStatus
        //         },
        //         commandType: CommandType.StoredProcedure);

        //     if (result == null)
        //         throw new Exception("No employee record returned from update");
        //     return _mapper.Map<EmployeeFullProfileViewModel>(result);
        // }

        public EmployeeFullProfileModel UpdateEmpDetails(EmployeeFullProfileViewModel updateEmpDetailsVM, out int status, out string message)
        {
            var updateEmpDetailsDM = new EmployeeFullProfileModel();
            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();

                    var parameters = new DynamicParameters();

                    // Map ViewModel → SP parameters explicitly
                    parameters.Add("@EmpPk", updateEmpDetailsVM.empPk, DbType.Int64);
                    parameters.Add("@EmpCode", updateEmpDetailsVM.empCode, DbType.String);
                    parameters.Add("@FirstName", updateEmpDetailsVM.firstName, DbType.String);
                    parameters.Add("@LastName", updateEmpDetailsVM.lastName, DbType.String);
                    parameters.Add("@DateOfBirth", updateEmpDetailsVM.dateOfBirth, DbType.Date);
                    parameters.Add("@Gender", updateEmpDetailsVM.gender, DbType.String);
                    parameters.Add("@MaritalStatus", updateEmpDetailsVM.maritalStatus, DbType.String);
                    parameters.Add("@Nationality", updateEmpDetailsVM.nationality, DbType.String);
                    parameters.Add("@PhoneNumber", updateEmpDetailsVM.phoneNumber, DbType.String);
                    parameters.Add("@AlternateNumber", updateEmpDetailsVM.alternateNumber, DbType.String);
                    parameters.Add("@Email", updateEmpDetailsVM.email, DbType.String);
                    parameters.Add("@StreetAddress", updateEmpDetailsVM.streetAddress, DbType.String);
                    parameters.Add("@City", updateEmpDetailsVM.city, DbType.String);
                    parameters.Add("@State", updateEmpDetailsVM.state, DbType.String);
                    parameters.Add("@ZipCode", updateEmpDetailsVM.zipCode, DbType.String);
                    parameters.Add("@Country", updateEmpDetailsVM.country, DbType.String);
                    parameters.Add("@Designation", updateEmpDetailsVM.designation, DbType.String);
                    parameters.Add("@TeamHRHead", updateEmpDetailsVM.teamHRHead, DbType.String);
                    parameters.Add("@ProjectManager", updateEmpDetailsVM.projectManager, DbType.String);
                    parameters.Add("@TeamLead", updateEmpDetailsVM.teamLead, DbType.String);
                    parameters.Add("@JobTitle", updateEmpDetailsVM.jobTitle, DbType.String);
                    parameters.Add("@EmploymentStatus", updateEmpDetailsVM.employmentStatus, DbType.String);
                    parameters.Add("@JoinedDate", updateEmpDetailsVM.joinedDate, DbType.Date);
                    parameters.Add("@Skillset", updateEmpDetailsVM.skillset, DbType.String);
                    parameters.Add("@PayGrade", updateEmpDetailsVM.payGrade, DbType.String);
                    parameters.Add("@Currency", updateEmpDetailsVM.currency, DbType.String);
                    parameters.Add("@BasicSalary", updateEmpDetailsVM.basicSalary, DbType.Decimal);
                    parameters.Add("@PayFrequency", updateEmpDetailsVM.payFrequency, DbType.String);
                    parameters.Add("@EmpStatus", updateEmpDetailsVM.empStatus, DbType.String);

                    // Output parameters
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    // Execute SP
                    //con.Execute("LMS_UpdateLeaveApplication", parameters, commandType: CommandType.StoredProcedure);

                    var result = con.Query<EmployeeFullProfileModel>(
                        SPConstants.updateFullEmployeeDetails,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Get outputs
                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");

                    // Map back to DataModel for return
                    updateEmpDetailsDM = _mapper.Map<EmployeeFullProfileModel>(updateEmpDetailsVM);
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message; // Provide exception message
            }

            return updateEmpDetailsDM;
        }
    }
}