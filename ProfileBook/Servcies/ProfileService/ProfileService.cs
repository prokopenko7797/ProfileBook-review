using ProfileBook.Constants;
using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using ProfileBook.Servcies.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProfileBook.Enums;
using ProfileBook.Servcies.Authorization;

namespace ProfileBook.Servcies.ProfileService
{
    public class ProfileService: IProfileService
    {
        private readonly IRepository<Profile> _repository;
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthorizationService _authorizationService;

        public ProfileService(IRepository<Profile> repository, ISettingsManager settingsManager,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _settingsManager = settingsManager;
            _authorizationService = authorizationService;
        }


        public async Task<bool> AddEdit(Profile profile)
        {
            if (profile.id != default)
            { 
                if (await _repository.Update(profile) != Constant.SQLError)
                    return true; 
            }
            else
            {
                if (await _repository.Insert(profile) != Constant.SQLError)
                    return true;
            }
            return false;
        }

        public async Task<bool> Dalete(int id) 
        {
            if (await _repository.Delete(id) != Constant.SQLError)
                return true;
            else return false;
        }

        public async Task<Profile> GetProfile(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Profile>> GetUserProfiles()
        {
            return await _repository.Query($"SELECT * FROM {nameof(Profile)} " +
                $"WHERE user_id='{_authorizationService.IdUser}'");
        }

        public async Task<IEnumerable<Profile>> GetUserSortedProfiles()
        {
            IEnumerable<Profile> p = await GetUserProfiles();

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
    }
}
