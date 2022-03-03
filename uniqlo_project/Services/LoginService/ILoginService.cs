using System;
using System.Threading.Tasks;

namespace uniqlo_project.Services.LoginService
{
    public interface ILoginService
    {
        Task<object> SignInAsync(string login, string password);
    }
}
