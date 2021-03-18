using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Servcies.Registration
{
    public class RegistrationService : IRegistrationService
    {
        #region _____Services______
        private readonly IRepository _repository;
        #endregion

        public RegistrationService(IRepository repository)
        {
            _repository = repository;
        }


        #region ______Public Methods______
        public async Task<bool>  RegistrateAsync(string login, string password)
        {
            
            User user = await _repository.FindWithQueryAsync<User>($"SELECT * FROM {nameof(User)} WHERE login='{login}'");

            if (user != null)
            {

                return false;
            }

            await _repository.InsertAsync(new User { Login = login, Password = password });
            return true;
        }

        #endregion
    }
}
