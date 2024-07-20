using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PilotTasksUpworkService.Services.Interface
{
    public interface IProfileService
    {
        Task<Result<bool>> AddProfileAsync(Profile profile);
        Task<Result<bool>> UpdateProfileAsync(Profile profile);
        Task<Result<bool>> DeleteProfileAsync(int ProfileId);
        Task<Result<IEnumerable<Profile>>> GetProfilesASync(Profile profile);
        Task<Result<Profile>> GetProfilesByIdASync(int ProfileId);
    }
}
