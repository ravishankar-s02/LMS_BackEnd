using Dapper;
using Quartz;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class AutoUpdateLeaveSummaryJob : IJob
{
    private readonly string _connectionString;

    public AutoUpdateLeaveSummaryJob(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.ExecuteAsync("SS_AutoUpdateLeaveSummary_SP", commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
