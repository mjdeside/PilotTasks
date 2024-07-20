using PilotTasksUpworkService.Model.Common;
using PilotTasksUpworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Services.Interface
{
    public interface ITasksService
    {
        Task<Result<bool>> AddTasksAsync(Tasks tasks);
        Task<Result<bool>> UpdateTasksAsync(Tasks tasks);
        Task<Result<bool>> DeleteTasksAsync(int Id);
        Task<Result<IEnumerable<TasksView>>> GetTasksASync(Tasks tasks);
        Task<Result<TasksView>> GetTasksByIdASync(int Id);
    }
}
