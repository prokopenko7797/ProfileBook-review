using ProfileBook.Constants;
using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Servcies.Repository;
using ProfileBook.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Servcies.Registration
{
    public class RegistrationService : IRegistrationService
    {

        private readonly IRepository<User> _repository;
 

        public RegistrationService(IRepository<User> repository)
        {
            _repository = repository;
        }



        public async Task<ValidEnum>  Registrate(string login, string password, string confirmpassword)
        {
            if (!Validator.InRange(login, Constant.MinLoginLength, Constant.MaxLoginLength))
            {   
                return ValidEnum.NotInRangeLogin;
            }

            if (!Validator.InRange(password, Constant.MinPasswordLength, Constant.MaxPasswordLength))
            {
                return ValidEnum.NotInRangePassword;
            }


            if (Validator.StartWithNumeral(login))
            {       
                return ValidEnum.StartWithNum;
            }

            if (!Validator.HasUpLowNum(password))
            {
                return ValidEnum.HasntUpLowNum;
            }



            if (!Validator.Match(password, confirmpassword))
            {
                
                return ValidEnum.HasntMach;
            }



            User user = await _repository.FindWithQuery($"SELECT * FROM {nameof(User)} WHERE login='{login}'");

            if (user != null)
            {
                
                return ValidEnum.LoginExist;
            }

            await _repository.Insert(new User { Login = login, Password = password });
            return ValidEnum.Success;
        }
    }
}
