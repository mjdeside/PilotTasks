
using System.Data;
using static Dapper.SqlMapper;

namespace PilotTasksUpworkService.DatabaseAccess
{
    public interface IDatabaseContext
    {
        Task<IEnumerable<T>> GetData<T, P>(string storedProcedureName, P parameters);
        Task ExecuteStoredProcedure<P>(string storedProcedureName, P parameters);
    }
}
