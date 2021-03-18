using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Servcies.ProfileService
{
    public interface IProfileService
    {
        Task<bool> DeleteAsync(int id);
        Task<bool> AddAsync(Profile profile);

        Task<bool> EditAsync(Profile profile);

        Task<Profile> GetProfileAsync(int id);
        Task<IEnumerable<Profile>> GetUserProfilesAsync();
        Task<IEnumerable<Profile>> GetUserSortedProfilesAsync();


    }
}
