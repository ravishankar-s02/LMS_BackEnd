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
    public class LeaveManagementService : ILeaveManagementService
    {
        private readonly IDbConnection _db;

        public LeaveManagementService(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "SS_ApplyLeave_SP",
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
        public async Task<IEnumerable<MyLeaveHistoryViewModel>> GetMyLeaveHistoryAsync(string empCode)
        {
            var result = await _db.QueryAsync<MyLeaveHistoryViewModel>(
                "SS_GetMyLeaveHistory_SP",
                new { EmpCode = empCode },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync(string empCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_Code", empCode);

            var result = await _db.QueryAsync<UsersLeaveHistoryViewModel>(
                "SS_GetUsersLeaveHistory_SP",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeaveId", model.leaveId);
            parameters.Add("@EmpCode", model.empCode);
            parameters.Add("@LeaveType", model.leaveType);
            parameters.Add("@FromDate", model.fromDate);
            parameters.Add("@ToDate", model.toDate);
            parameters.Add("@Reason", model.reason);
            parameters.Add("@Duration", model.duration);

            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "SS_UpdateLeaveApplication_SP", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeaveId", model.leaveId);
            parameters.Add("@EmpCode", model.empCode);

            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "SS_DeleteLeaveApplication_SP", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<LeaveActionViewModel>> GetLeaveActionAsync(string empCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SS_Emp_Code", empCode);

            var result = await _db.QueryAsync<LeaveActionViewModel>(
                "SS_LeaveAction_SP",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }


        public async Task<IEnumerable<LeaveActionViewModel>> UpdateLeaveActionAsync(LeaveActionRequestModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeavePK", model.LeavePK);
            parameters.Add("@Action", model.Action?.ToUpper());
            parameters.Add("@SS_Emp_Code", model.EmpCode);  // 👈 Add this line

            var result = await _db.QueryAsync<LeaveActionViewModel>(
                "SS_LeaveAction_SP",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}