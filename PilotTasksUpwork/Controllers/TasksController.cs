using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Model.Common;
using PilotTasksUpworkService.Model.Utility;
using PilotTasksUpworkService.Services;
using PilotTasksUpworkService.Services.Interface;
using System.Reflection;
using System.Threading.Tasks;

namespace PilotTasksUpwork.Controllers
{
    public class TasksController : Controller
    {

        private ITasksService _tasksService;
        private IProfileService _profileService;
        public TasksController(ITasksService tasksService, IProfileService profileService)
        {
            _tasksService = tasksService;
            _profileService = profileService;
        }


        public async Task<ActionResult> List(TasksView tasks)
        {
            var getData = await _tasksService.GetTasksASync(tasks);
            if (getData.IsSucess)
            {
                return View("Task", model: getData.Data);
            }
            else
            {
                return View("Error");
            }
            
        }
      
        public async Task<ActionResult> NewTask()
        {
            return View(await InitializeModel());
        }
        private async Task<CreateTasksModel> InitializeModel()
        {
            var getProfile = await _profileService.GetProfilesASync(new Profile());
            var CreateTasksModel = new CreateTasksModel() { };
            List<StatusDropDown> statusDropDowns = new List<StatusDropDown>();

            if (getProfile.IsSucess)
            {
                if (getProfile.Data != null && getProfile.Data.Any())
                {
                    getProfile.Data = getProfile.Data.OrderBy(e => e.LastName);
                    CreateTasksModel.ProfileList = getProfile.Data.Select(e => new ProfileDropdown()
                    {
                        DisplayName = $@"{e.LastName},{e.FirstName}",
                        ProfileId = e.ProfileId
                    }).ToList();
                }
            }
            List<Status> statusList = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            foreach (var status in statusList)
            {
                statusDropDowns.Add(new StatusDropDown()
                {
                    StatusId = (int)status,
                    StatusDisplay = status.GetDescription()
                });
            }
            CreateTasksModel.StatusList = statusDropDowns;
            return CreateTasksModel;
        }


        [HttpPost]
      
        public async Task<ActionResult> CreateTask(CreateTasksModel model)
        {
            var save = await _tasksService.AddTasksAsync(new Tasks()
            {
                ProfileId = model.ProfileId,
                StartTime = model.StartTime,
                Status = model.Status,
                TaskDescription = model.TaskDescription,
                TaskName = model.TaskName,
            });
            if (save.IsSucess)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = save.Message;
                return View("NewTask");
            }
        }

        public async Task<ActionResult> TasksByProfileId(int Id)
        {
            var getData = await _tasksService.GetTasksASync(new Tasks()
            {
                ProfileId= Id
            });
            if (getData.IsSucess)
            {
                return View("TasksByProfile", model: getData.Data);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var getData = await _tasksService.GetTasksByIdASync(id);

            var tasksForm = await InitializeModel();

            if (getData.IsSucess)
            {
                tasksForm.TaskDescription = getData.Data.TaskDescription;
                tasksForm.Status = getData.Data.Status;
                tasksForm.TaskName = getData.Data.TaskName;
                tasksForm.StartTime= getData.Data.StartTime;
                tasksForm.Id = getData.Data.Id; 
                tasksForm.ProfileId= getData.Data.ProfileId;
                return View("Edit", tasksForm);
            }
            else
            {
                TempData["msg"] = getData.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTasks(CreateTasksModel model)
        {
            var save = await _tasksService.UpdateTasksAsync(new Tasks()
            {
                ProfileId = model.ProfileId,
                Id= model.Id,
                StartTime = model.StartTime,
                Status = model.Status,
                TaskDescription = model.TaskDescription,
                TaskName = model.TaskName,
            });
            if (save.IsSucess)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = save.Message;
                return View("NewTask");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var save = await _tasksService.DeleteTasksAsync(id);
            if (save.IsSucess)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = save.Message;
                return View("Error");
            }
        }
    }
}
