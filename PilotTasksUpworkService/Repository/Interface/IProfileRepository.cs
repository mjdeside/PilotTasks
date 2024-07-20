using PilotTasksUpworkService.Model;


namespace PilotTasksUpworkService.Repository.Interface
{
    public interface IProfileRepository
    {
        Task<bool> AddProfileAsync(Profile profile);
        Task<bool> UpdateProfileAsync(Profile profile);
        Task<bool> DeleteProfile(int ProfileId);
        Task<IEnumerable<Profile>> GetProfilesASync(Profile profile);
    }
}
