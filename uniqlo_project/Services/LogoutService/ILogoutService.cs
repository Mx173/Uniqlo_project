using System;
using System.Threading.Tasks;

namespace uniqlo_project.Services.LogoutService
{
    public interface ILogoutService
    {
        Task<string> SignOutAsync();
    }
}
