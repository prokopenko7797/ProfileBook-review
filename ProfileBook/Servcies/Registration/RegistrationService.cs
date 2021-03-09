using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Servcies.Registration
{
    public class RegistrationService : IRegistrationService
    {
        #region _____Services______
        private readonly IRepository<User> _repository;
        #endregion

        public RegistrationService(IRepository<User> repository)
        {
            _repository = repository;
        }


        #region ______Public Methods______
        public async Task<bool>  Registrate(string login, string password)
        {
            
            User user = await _repository.FindWithQuery($"SELECT * FROM {nameof(User)} WHERE login='{login}'");

            if (user != null)
            {

                return false;
            }

            await _repository.Insert(new User { Login = login, Password = password });
            return true;
        }

        #endregion
    }
}
