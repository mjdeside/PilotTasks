using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Model.Common;
using PilotTasksUpworkService.Model.Utility;
using PilotTasksUpworkService.Repository;
using PilotTasksUpworkService.Repository.Interface;
using PilotTasksUpworkService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }
        public async Task<Result<bool>> AddTasksAsync(Tasks tasks)
        {
            try
            {
                var IsSave = await _tasksRepository.AddTaskAsync(tasks);
                return new Result<bool>()
                {
                    Data = IsSave,
                    IsSucess = IsSave,
                    Message = IsSave ? "The Tasks is successfully created" : "The Tasks is fail to create"
                };

            }
            catch(Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    IsSucess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result<bool>> DeleteTasksAsync(int Id)
        {
            try
            {
                var IsSave = await _tasksRepository.DeleteTaskAsync(Id);
                return new Result<bool>()
                {
                    Data = IsSave,
                    IsSucess = IsSave,
                    Message = IsSave ? "The Tasks is successfully deleted" : "The Tasks is fail to deleted"
                };

            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    IsSucess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result<IEnumerable<TasksView>>> GetTasksASync(Tasks tasks)
        {
            try
            {
                var getData = await _tasksRepository.GetTasksASync(tasks);

                if (getData != null && getData.Any())
                {
                    var displayData = getData.Select(e => ConvertTasksIntoTaskView(e));
                    return new Result<IEnumerable<TasksView>>()
                    {
                        Data = displayData,
                        IsSucess = true,
                    };
                }
                else
                {
                    return new Result<IEnumerable<TasksView>>()
                    {
                        Data = new List<TasksView>(),
                        IsSucess = true,
                    };
                    
                }

            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<TasksView>>()
                {
                    IsSucess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result<TasksView>> GetTasksByIdASync(int Id)
        {
            try
            {
                var getData = await _tasksRepository.GetTasksASyncById(Id);

                if(getData != null)
                {
                    return new Result<TasksView>()
                    {
                        Data = ConvertTasksIntoTaskView(getData),
                        IsSucess = true,
                    };

                }
                else
                {
                    return new Result<TasksView>()
                    {
                        IsSucess = false,
                        Message = "Tasks does not exist"
                    };

                }

            }
            catch (Exception ex)
            {
                return new Result<TasksView>()
                {
                    IsSucess = false,
                    Message = ex.Message
                };
            }
        }
    
        public async Task<Result<bool>> UpdateTasksAsync(Tasks tasks)
        {
            try
            {
                var IsSave = await _tasksRepository.UpdateTaskAsync(tasks);
                return new Result<bool>()
                {
                    Data = IsSave,
                    IsSucess = IsSave,
                    Message = IsSave ? "The tasks is successfully updated" : "The Tasks is fail to update"
                };

            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    IsSucess = false,
                    Message = ex.Message
                };
            }
        }
        private TasksView ConvertTasksIntoTaskView(Tasks e)
        {
            if (e != null)
            {
                return new TasksView()
                {
                    TaskDescription = e.TaskDescription,
                    TaskName = e.TaskName,
                    StartTime = e.StartTime,
                    ProfileId = e.ProfileId,
                    Id = e.Id,
                    TasksAssignee = $@"{e.LastName},{e.FirstName}",
                    StartTimeDisplay = e.StartTime.HasValue? $@"{ e.StartTime.Value.ToShortDateString()} {e.StartTime.Value.ToShortTimeString()}" : "",
                    Status = e.Status,
                    StatusDisplay = ((Status)e.Status).GetDescription()
                };
            }
            else
            {
                return new TasksView();
            }

        }

    }
}
