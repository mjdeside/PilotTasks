using Dapper;
using Microsoft.Extensions.Configuration;
using PilotTasksUpworkService.DatabaseAccess;
using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Model.Common;
using PilotTasksUpworkService.Repository.Interface;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace PilotTasksUpworkService.Repository
{

    public class TasksRepository : ITasksRepository
    {
        private readonly IDatabaseContext _dbContext;
     
        public TasksRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
          

        }

        public async Task<bool> AddTaskAsync(Tasks task)
        {
            await _dbContext.ExecuteStoredProcedure("usp_InsertOrUpdateTask", new { task.TaskName, task.TaskDescription, task.ProfileId, task.StartTime, task.Status });
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int Id)
        {
            await _dbContext.ExecuteStoredProcedure("usp_DeleteTasks", new { Id });
            return true;
        }

        public async Task<IEnumerable<Tasks>> GetTasksASync(Tasks task)
        {
            return await _dbContext.GetData<Tasks, dynamic>("usp_GetFilteredTasks", new { task.ProfileId, task.TaskName, task.TaskDescription, task.StartTime, task.Status });
        }

        public async Task<Tasks> GetTasksASyncById(int Id)
        {
            return (await GetTasksASync(new Tasks() { Id = Id})).FirstOrDefault();

        }
        public async Task<bool> UpdateTaskAsync(Tasks task)
        {
            await _dbContext.ExecuteStoredProcedure("usp_InsertOrUpdateTask", new { task.Id, task.TaskName, task.TaskDescription, task.ProfileId, task.StartTime, task.Status });
            return true;
        }
    }
}
