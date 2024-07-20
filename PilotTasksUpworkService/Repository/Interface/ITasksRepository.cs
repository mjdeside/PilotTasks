using PilotTasksUpworkService.Model;


namespace PilotTasksUpworkService.Repository.Interface
{
    public interface ITasksRepository
    {
        Task<bool> AddTaskAsync(Tasks profile);
        Task<bool> UpdateTaskAsync(Tasks profile);
        Task<bool> DeleteTaskAsync(int Id);
        Task<IEnumerable<Tasks>> GetTasksASync(Tasks profile);
        Task<Tasks> GetTasksASyncById(int id);

    }
}
