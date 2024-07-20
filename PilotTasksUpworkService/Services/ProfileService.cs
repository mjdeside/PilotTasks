using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Model.Common;
using PilotTasksUpworkService.Repository.Interface;
using PilotTasksUpworkService.Services.Interface;
namespace PilotTasksUpworkService.Services
{
    public class ProfileService : IProfileService
    {

        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<Result<bool>> AddProfileAsync(Profile profile)
        {
            try
            {
                var IsProfileAlreadyExist = await _profileRepository.GetProfilesASync(new Profile()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                });

                if (IsProfileAlreadyExist.Any() && !IsProfileAlreadyExist.Any(e => e.ProfileId == profile.ProfileId))
                {
                    return new Result<bool>()
                    {
                        Data = false,
                        IsSucess = false,
                        Message = "Profile already exist"
                    };
                }

                var isSave = await _profileRepository.AddProfileAsync(profile);

                var result = new Result<bool>()
                {
                    Data = isSave,
                    IsSucess = false,
                    Message = isSave ? "Successfully Added" : "Fail to add"
                };

                return result;
            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    Message = ex.Message,
                    IsSucess = false
                };
            }
        }

        public async Task<Result<bool>> DeleteProfileAsync(int ProfileId)
        {
            try
            {
                var isSave = await _profileRepository.DeleteProfile(ProfileId);
                var result = new Result<bool>()
                {
                    Data = isSave,
                    IsSucess = false,
                };

                return result;
            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    Message = ex.Message,
                    IsSucess = false

                };
            }
        }

        public async Task<Result<IEnumerable<Profile>>> GetProfilesASync(Profile profile)
        {
            try
            {
                var data = await _profileRepository.GetProfilesASync(profile);
                var result = new Result<IEnumerable<Profile>>()
                {
                    Data = data,
                    IsSucess = true,

                };

                return result;
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Profile>>()
                {
                    Data = new List<Profile>(),
                    Message = ex.Message,
                    IsSucess = false
                };
            }
        }

        public async Task<Result<Profile>> GetProfilesByIdASync(int ProfileId)
        {
            try
            {
                var result = new Result<Profile>() { };
                var data = await _profileRepository.GetProfilesASync(new Profile()
                {
                    ProfileId = ProfileId
                });

                if (data != null && data.Any())
                {
                    result = new Result<Profile>()
                    {
                        Data = data.FirstOrDefault(),
                        IsSucess = true,
                    };
                }
                else
                {
                    result = new Result<Profile>()
                    {
                        Data = new Profile(),
                        IsSucess = false,
                    };
                }
                return result;
            }
            catch (Exception ex)
            {
                return new Result<Profile>()
                {
                    Data = new Profile(),
                    Message = ex.Message,
                    IsSucess = false
                };
            }
        }

        public async Task<Result<bool>> UpdateProfileAsync(Profile profile)
        {
            try
            {
                var isSave = await _profileRepository.UpdateProfileAsync(profile);
                var result = new Result<bool>()
                {
                    Data = isSave,
                    IsSucess = false,
                    Message = isSave ? "Successfully Update" : "Fail to Update"
                };

                return result;
            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Data = false,
                    Message = ex.Message,
                    IsSucess = false

                };
            }
        }
    }
}
