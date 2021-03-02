using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook.Constants;
using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using ProfileBook.Servcies.Settings;
using Xamarin.Essentials;

namespace ProfileBook.Servcies.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository<User> _repository;
        private readonly ISettingsManager _settingsManager;

        public AuthorizationService(IRepository<User> repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }
        public async Task<bool> Authorize(string login, string password)
        {

            var user = await _repository.FindWithQuery($"SELECT * FROM {nameof(User)} WHERE login='{login}' AND password='{password}'");

            if(user != null)
            {
                IdUser = user.id;
                return true;
            }

            return false;
        }

        public bool IsAuthorize()
        {
            if (IdUser == Constant.NonAuthorized) return true;
            else return false;
        }

        public void LogOut()
        {
            IdUser = Constant.NonAuthorized;
        }

        public int IdUser
        {
            get => Preferences.Get(nameof(IdUser), Constant.NonAuthorized);
            set => Preferences.Set(nameof(IdUser), value);
        }
    }
}
