using Microsoft.AspNetCore.Mvc;
using PilotTasksUpwork.Models;
using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Services.Interface;

namespace PilotTasksUpwork.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
   
        public async Task<IActionResult> List(Profile profile)
        {
            var getData = await _profileService.GetProfilesASync(profile);
            if (getData.IsSucess)
            {
                return View("Profile", model: getData.Data);
            }
            else
            {
                return View("Error");
            } 
           
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var getData = await _profileService.GetProfilesByIdASync(Id);
            if (getData.IsSucess)
            {
                return View(getData.Data);
            }
            else
            {
                return View("Error",new ErrorViewModel()
                {
                    Message = "Profile doesn't exist"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Profile profile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(profile);
                }
                else
                {
                    var addProfile = await _profileService.UpdateProfileAsync(profile);
                    TempData["msg"] = addProfile.Message;
                    if (!addProfile.IsSucess)
                    {
                        return View();
                    }
                }
            }
            catch
            {

            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> NewProfile()
        {
            return View();

        }
        public async Task<IActionResult> Delete(int Id)
        {
            await _profileService.DeleteProfileAsync(Id);
            return RedirectToAction(nameof(List));

        }
        [HttpPost]
        public async Task<IActionResult> NewProfile(Profile profile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(profile);
                }
                else
                {
                    var addProfile = await _profileService.AddProfileAsync(profile);
                    TempData["msg"] = addProfile.Message;
                    if (!addProfile.IsSucess)
                    {
                        return View();
                    }
                }
            }
            catch
            {

            }
            return RedirectToAction(nameof(List));
        }
    }
}
