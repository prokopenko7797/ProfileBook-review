using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Servcies.Registration
{
    public interface IRegistrationService
    {
        Task<ValidEnum> Registrate(string login, string password, string confirmpassword);
    }
}
