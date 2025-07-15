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
                "Sp_ApplyLeave_SS",
                new
                {
                    EmpFK = model.EmpFK,
                    EmpCode = model.EmpCode,
                    LeaveType = model.LeaveType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Reason = model.Reason,
                    InsertedUser = model.InsertedUser
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}