using System;
using System.Threading.Tasks;

namespace uniqlo_project.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<object> SignUpAsync(string login, string password, string email, string first_name, string gender, string promo);
    }
}
