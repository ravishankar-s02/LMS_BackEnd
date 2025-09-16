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
    public class LeaveManagementService : ILeaveManagementService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _db;
        private readonly IMapper _mapper;

        public LeaveManagementService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _mapper = mapper;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString(Constants.databaseName));
            }
        }
        public long ApplyLeave(LeaveApplicationViewModel leaveApplication, out int status, out string message)
        {
            // long leavePk = 0;
            status = 0;
            message = string.Empty;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    // Map model to parameters
                    parameters.Add("@EmpCode", leaveApplication.empCode, DbType.String);
                    parameters.Add("@LeaveType", leaveApplication.leaveType, DbType.String);
                    parameters.Add("@FromDate", leaveApplication.fromDate, DbType.Date);
                    parameters.Add("@ToDate", leaveApplication.toDate, DbType.Date);
                    parameters.Add("@FromTime", leaveApplication.fromTime, DbType.String);
                    parameters.Add("@ToTime", leaveApplication.toTime, DbType.String);
                    parameters.Add("@TotalHours", decimal.TryParse(leaveApplication.totalHours, out var th) ? th : (decimal?)null, DbType.Decimal);
                    parameters.Add("@Duration", leaveApplication.duration, DbType.String);
                    parameters.Add("@Reason", leaveApplication.reason, DbType.String);

                    // Output params
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    // parameters.Add("@LeavePk", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 1);

                    // Call stored procedure
                    con.Execute(SPConstants.applyLeave, parameters, commandType: CommandType.StoredProcedure);

                    // Get output values
                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                    // leavePk = parameters.Get<Int64>("@LeavePk");
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message;
            }

            return status;
        }

        public List<MyLeaveHistoryViewModel> GetMyLeaveHistoryByEmpId(string empCode, out int status, out string message)
        {
            var myLeaveHistoryVMs = new List<MyLeaveHistoryViewModel>();
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

                    var result = con.Query<MyLeaveHistoryModel>(
                        SPConstants.myLeaveHistory,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    myLeaveHistoryVMs = result.Select(r => _mapper.Map<MyLeaveHistoryViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return myLeaveHistoryVMs;
        }

        public List<UsersLeaveHistoryViewModel> GetUsersLeaveHistoryByEmpId(string empCode, out int status, out string message)
        {
            var usersLeaveHistoryVMs = new List<UsersLeaveHistoryViewModel>();
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

                    var result = con.Query<UsersLeaveHistoryModel>(
                        SPConstants.usersLeaveHistory,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    usersLeaveHistoryVMs = result.Select(r => _mapper.Map<UsersLeaveHistoryViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return usersLeaveHistoryVMs;
        }

        public LeaveUpdateModel UpdateLeave(LeaveUpdateViewModel updateLeaveViewModel, out int status, out string message)
        {
            var updateLeaveDM = new LeaveUpdateModel();
            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();

                    var parameters = new DynamicParameters();

                    // Map ViewModel → SP parameters explicitly
                    parameters.Add("@LeaveId", updateLeaveViewModel.leaveId, DbType.Int64);
                    parameters.Add("@EmpCode", updateLeaveViewModel.empCode, DbType.String);
                    parameters.Add("@LeaveType", updateLeaveViewModel.leaveType, DbType.String);
                    parameters.Add("@FromDate", updateLeaveViewModel.fromDate, DbType.Date);
                    parameters.Add("@ToDate", updateLeaveViewModel.toDate, DbType.Date);

                    parameters.Add("@FromTime", updateLeaveViewModel.fromTime, DbType.String);
                    parameters.Add("@ToTime", updateLeaveViewModel.toTime, DbType.String);
                    parameters.Add("@TotalHours", updateLeaveViewModel.totalHours, DbType.Int64);

                    parameters.Add("@Reason", updateLeaveViewModel.reason, DbType.String);
                    parameters.Add("@Duration", updateLeaveViewModel.duration, DbType.String);

                    // Output parameters
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    // Execute SP
                    //con.Execute("LMS_UpdateLeaveApplication", parameters, commandType: CommandType.StoredProcedure);

                    var result = con.Query<LeaveUpdateViewModel>(
                        SPConstants.updateLeaveApplication,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Get outputs
                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");

                    // Map back to DataModel for return
                    updateLeaveDM = _mapper.Map<LeaveUpdateModel>(updateLeaveViewModel);
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message; // Provide exception message
            }

            return updateLeaveDM;
        }
        public LeaveDeleteModel DeleteLeave(LeaveDeleteViewModel deleteLeaveViewModel, out int status, out string message)
        {
            var deleteLeaveDM = new LeaveDeleteModel();
            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();

                    var parameters = new DynamicParameters();

                    parameters.Add("@LeavePk", deleteLeaveViewModel.leaveId, DbType.Int64);
                    parameters.Add("@EmpCode", deleteLeaveViewModel.empCode, DbType.String);

                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = con.Query<LeaveDeleteModel>(
                        SPConstants.deleteLeaveApplication,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");

                    deleteLeaveDM = _mapper.Map<LeaveDeleteModel>(deleteLeaveViewModel);
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message; // Provide exception message
            }

            return deleteLeaveDM;
        }

        public List<LeaveActionViewModel> GetLeaveRequestByEmpId(string empCode, out int status, out string message)
        {
            var leaveRequestVMs = new List<LeaveActionViewModel>();
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

                    var result = con.Query<LeaveActionModel>(
                        SPConstants.leaveRequest,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    leaveRequestVMs = result.Select(r => _mapper.Map<LeaveActionViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return leaveRequestVMs;
        }

        // public long UpdateLeaveRequest(LeaveActionRequestViewModel leaveRequestUpdate, out int status, out string message)
        // {
        //     status = 0;
        //     message = string.Empty;

        //     try
        //     {
        //         using (IDbConnection con = Connection)
        //         {
        //             con.Open();
        //             var parameters = new DynamicParameters();

        //             // Map model to parameters
        //             parameters.Add("@LeavePK", leaveRequestUpdate.LeaveId, DbType.Int64);
        //             parameters.Add("@Action", leaveRequestUpdate.Action, DbType.String);
        //             parameters.Add("@SS_Emp_Code", leaveRequestUpdate.Empcode, DbType.String);

        //             parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
        //             parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //             con.Execute(SPConstants.updateLeaveRequest, parameters, commandType: CommandType.StoredProcedure);

        //             // Get output values
        //             status = parameters.Get<Int16>("@Status");
        //             message = parameters.Get<string>("@ErrMsg");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         status = -1;
        //         message = ex.Message;
        //     }

        //     return status;
        // }
        public object UpdateLeaveRequest(LeaveActionRequestViewModel leaveRequestUpdate, out int status, out string message)
        {
            status = 0;
            message = string.Empty;

            object leaveRequestUpdateDm = null;

            try
            {
                using (IDbConnection con = Connection)
                {
                    con.Open();
                    var parameters = new DynamicParameters();

                    // Map model to parameters
                    parameters.Add("@LeavePK", leaveRequestUpdate.LeaveId, DbType.Int64);
                    parameters.Add("@Action", leaveRequestUpdate.Action, DbType.String);
                    parameters.Add("@SS_Emp_Code", leaveRequestUpdate.EmpCode, DbType.String);

                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    // Use QuerySingleOrDefault to get SP return value
                    leaveRequestUpdateDm = con.QuerySingleOrDefault<object>(
                        SPConstants.updateLeaveRequest, 
                        parameters, 
                        commandType: CommandType.StoredProcedure
                    );

                    // Get output values
                    status = parameters.Get<short>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message;
            }

            return leaveRequestUpdateDm;
        }

        public List<MyLeaveSummaryViewModel> GetLeaveSummaryByEmpId(string empCode, out int status, out string message)
        {
            var leaveSummaryVMs = new List<MyLeaveSummaryViewModel>();
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

                    var result = con.Query<MyLeaveSummaryModel>(
                        SPConstants.leaveSummary,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    // ✅ Map Model → ViewModel
                    leaveSummaryVMs = result.Select(r => _mapper.Map<MyLeaveSummaryViewModel>(r)).ToList();

                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");
                }
            }
            catch (Exception)
            {
                status = 5;
                message = "An Exception Thrown";
            }

            return leaveSummaryVMs;
        }
    }
}