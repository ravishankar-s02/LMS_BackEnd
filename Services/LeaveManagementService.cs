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

        public async Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "LMS_ApplyLeave",
                new
                {
                    EmpCode = model.EmpCode,
                    LeaveType = model.LeaveType,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    FromTime = model.FromTime,
                    ToTime = model.ToTime,
                    TotalHours = model.TotalHours,  
                    Duration = model.Duration,      
                    Reason = model.Reason
                },
                commandType: CommandType.StoredProcedure);
            return result;
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

        // public async Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateViewModel model)
        // {
        //     var parameters = new DynamicParameters();
        //     parameters.Add("@LeaveId", model.leaveId);
        //     parameters.Add("@EmpCode", model.empCode);
        //     parameters.Add("@LeaveType", model.leaveType);
        //     parameters.Add("@FromDate", model.fromDate);
        //     parameters.Add("@ToDate", model.toDate);
        //     parameters.Add("@FromTime", model.fromTime);
        //     parameters.Add("@ToTime", model.toTime);
        //     parameters.Add("@TotalHours", model.totalHours);
        //     parameters.Add("@Reason", model.reason);
        //     parameters.Add("@Duration", model.duration);

        //     var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
        //         "LMS_UpdateLeaveApplication", parameters, commandType: CommandType.StoredProcedure);

        //     return result;
        // }
        public LeaveUpdateModel UpdateLeave(LeaveUpdateViewModel updateLeaveViewModel, out int status, out string message)
        {
            var leaveDM = new LeaveUpdateModel();
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

                    // **Fix: convert empty strings to NULL for FromTime/ToTime**
                    // parameters.Add("@FromTime", string.IsNullOrWhiteSpace(updateLeaveViewModel.fromTime) ? null : updateLeaveViewModel.fromTime, DbType.String);
                    // parameters.Add("@ToTime", string.IsNullOrWhiteSpace(updateLeaveViewModel.toTime) ? null : updateLeaveViewModel.toTime, DbType.String);

                    // **Fix: handle nullable TotalHours safely**
                    // parameters.Add("@TotalHours", updateLeaveViewModel.totalHours ?? 0, DbType.Decimal);

                    parameters.Add("@FromTime", updateLeaveViewModel.fromTime, DbType.String);
                    parameters.Add("@ToTime", updateLeaveViewModel.toTime, DbType.String);
                    parameters.Add("@TotalHours", updateLeaveViewModel.totalHours, DbType.Int64);

                    parameters.Add("@Reason", updateLeaveViewModel.reason, DbType.String);
                    parameters.Add("@Duration", updateLeaveViewModel.duration, DbType.String);

                    // Output parameters
                    parameters.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 1);
                    parameters.Add("@ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    // Execute SP
                    con.Execute("LMS_UpdateLeaveApplication", parameters, commandType: CommandType.StoredProcedure);

                    // Get outputs
                    status = parameters.Get<Int16>("@Status");
                    message = parameters.Get<string>("@ErrMsg");

                    // Map back to DataModel for return
                    leaveDM = _mapper.Map<LeaveUpdateModel>(updateLeaveViewModel);
                }
            }
            catch (Exception ex)
            {
                status = -1;
                message = ex.Message; // Provide exception message
            }

            return leaveDM;
        }



        public async Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeaveId", model.leaveId);
            parameters.Add("@EmpCode", model.empCode);

            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "LMS_DeleteLeaveApplication", parameters, commandType: CommandType.StoredProcedure);
            return result;
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


        public async Task<IEnumerable<LeaveActionViewModel>> UpdateLeaveActionAsync(LeaveActionRequestModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeavePK", model.LeavePK);
            parameters.Add("@Action", model.Action?.ToUpper());
            parameters.Add("@SS_Emp_Code", model.EmpCode);

            var result = await _db.QueryAsync<LeaveActionModel>(
                "LMS_UpdateLeaveRequest",
                parameters,
                commandType: CommandType.StoredProcedure);
            return _mapper.Map<IEnumerable<LeaveActionViewModel>>(result);
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