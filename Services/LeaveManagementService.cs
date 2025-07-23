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

        public async Task<(int Status, string Message)> ApplyLeaveAsync(LeaveApplicationViewModel model)
        {
            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "SS_ApplyLeave_SP",
                new
                {
                    EmpCode = model.empCode,
                    LeaveType = model.leaveType,
                    FromDate = model.fromDate,
                    ToDate = model.toDate,
                    FromTime = model.fromTime,
                    ToTime = model.toTime,
                    TotalHours = model.totalHours,     // Pass from frontend
                    Duration = model.duration,         // Pass from frontend
                    Reason = model.reason
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

        public async Task<IEnumerable<UsersLeaveHistoryViewModel>> GetUsersLeaveHistoryAsync()
        {
            var result = await _db.QueryAsync<UsersLeaveHistoryViewModel>(
                "SS_GetUsersLeaveHistory_SP",
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<(int Status, string Message)> UpdateLeaveAsync(LeaveUpdateModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeavePK", model.LeavePK);
            parameters.Add("@EmpCode", model.empCode);
            parameters.Add("@LeaveType", model.LeaveType);
            parameters.Add("@FromDate", model.fromDate);
            parameters.Add("@ToDate", model.toDate);
            parameters.Add("@Reason", model.Reason);

            var result = await _db.QueryFirstOrDefaultAsync<(int Status, string Message)>(
                "SS_UpdateLeaveApplication_SP", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<(int Status, string Message)> DeleteLeaveAsync(LeaveDeleteModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeavePK", model.LeavePK);
            parameters.Add("@EmpCode", model.empCode);

            var result = await _db.QueryFirstOrDefaultAsync<(int, string)>(
                "SS_DeleteLeaveApplication_SP", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}