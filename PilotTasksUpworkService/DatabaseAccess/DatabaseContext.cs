using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using static Dapper.SqlMapper;

namespace PilotTasksUpworkService.DatabaseAccess
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionId = "conn";
        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<T>> GetData<T, P>(string storedProcedureName, P parameters)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task ExecuteStoredProcedure<P>(string storedProcedureName, P parameters)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
           
        }
    }
}
