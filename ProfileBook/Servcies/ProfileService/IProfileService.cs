using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Servcies.ProfileService
{
    public interface IProfileService
    {
        Task<bool> Dalete(int id);
        Task<bool> AddEdit(Profile profile);
        Task<Profile> GetProfile(int id);
        Task<IEnumerable<Profile>> GetUserProfiles();
        Task<IEnumerable<Profile>> GetUserSortedProfiles();


    }
}
