using PilotTasksUpworkService.DatabaseAccess;
using PilotTasksUpworkService.Model;
using PilotTasksUpworkService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDatabaseContext _dbContext;
        public ProfileRepository(IDatabaseContext dbContext) {
        _dbContext = dbContext;
        }

        public async Task<bool> AddProfileAsync(Profile profile)
        {
            await _dbContext.ExecuteStoredProcedure("InsertOrUpdateProfile", new { profile.FirstName, profile.LastName, profile.DateOfBirth,profile.PhoneNumber, profile.EmailId });
            return true;
        }
        public async Task<bool> UpdateProfileAsync(Profile profile)
        {
            await _dbContext.ExecuteStoredProcedure("InsertOrUpdateProfile", new { profile.ProfileId, profile.FirstName, profile.LastName, profile.DateOfBirth, profile.PhoneNumber, profile.EmailId });
            return true;
        }

        public async Task<IEnumerable<Profile>> GetProfilesASync(Profile profile)
        {
            return await _dbContext.GetData<Profile, dynamic>("usp_GetFilteredProfile", new { profile.ProfileId, profile.FirstName, profile.LastName, profile.DateOfBirth, profile.EmailId });

        }

        public async Task<bool> DeleteProfile(int ProfileId)
        {
            await _dbContext.ExecuteStoredProcedure("usp_DeleteProfile",new { ProfileId});
            return true;
        }
    }
}
