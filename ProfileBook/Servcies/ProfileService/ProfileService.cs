using ProfileBook.Constants;
using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using ProfileBook.Servcies.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProfileBook.Enums;
using ProfileBook.Servcies.Authorization;

namespace ProfileBook.Servcies.ProfileService
{
    public class ProfileService: IProfileService
    {
        #region _____Services______
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        public ProfileService(IRepository repository, ISettingsManager settingsManager,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _settingsManager = settingsManager;
            _authorizationService = authorizationService;
        }

        #region ______Public Methods______
        public async Task<bool> EditAsync(Profile profile)
        {
            return (await _repository.UpdateAsync(profile) != Constant.SQLError);   
        }

        public async Task<bool> AddAsync(Profile profile) 
        {
            return (await _repository.InsertAsync(profile) != Constant.SQLError);
        }



        public async Task<bool> DeleteAsync(int id) 
        {
            if (await _repository.DeleteAsync<Profile>(id) != Constant.SQLError)
                return true;
            else return false;
        }

        public async Task<Profile> GetProfileAsync(int id)
        {
            return await _repository.GetByIdAsync<Profile>(id);
        }

        public async Task<IEnumerable<Profile>> GetUserProfilesAsync()
        {
            return await _repository.QueryAsync<Profile>($"SELECT * FROM {nameof(Profile)} " +
                $"WHERE user_id='{_authorizationService.IdUser}'");
        }

        public async Task<IEnumerable<Profile>> GetUserSortedProfilesAsync()
        {
            IEnumerable<Profile> p = await GetUserProfilesAsync();

            p = p.OrderBy(Profile => _settingsManager.SortBy);

            switch (_settingsManager.SortBy)
            {
                case (int)SortEnum.date:
                    p = p.OrderBy(Profile => Profile.date);
                    break;

                case (int)SortEnum.nick_name:
                    p = p.OrderBy(Profile => Profile.nick_name);
                    break;

                case (int)SortEnum.name:
                    p = p.OrderBy(Profile => Profile.name);
                    break;
                default:
                    break;
            }

            return p;
        }
        #endregion
    }
}
