using System;
using System.Net.Http;
using System.Threading.Tasks;
using uniqlo_project.Models;

namespace uniqlo_project.Services.UpdatePersonalDataService
{
    public interface IUpdatePersonalDataService
    {
        Task<UpdatePersonalDataResponse> UpdatePersonalData(string first_name, string gender, string email, string phone, string birth_date);
        Task<UpdateAvatarResponse> ChangePassword(string old_password, string new_password);
        Task<UpdateAvatarResponse> UpdateProfilePhoto(ByteArrayContent profile_photo);
        Task<UpdateAvatarResponse> ResPas(string loginn);
        Task<UpdateAvatarResponse> NewPas(string loginn, string keyy, string newpassword);
    }
}
