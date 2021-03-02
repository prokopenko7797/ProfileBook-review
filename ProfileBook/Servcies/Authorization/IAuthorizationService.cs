using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Servcies.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(string login, string password);
        bool IsAuthorize();
        void LogOut();

        int IdUser { get; set; }
    }
}
