using System;
using System.Threading.Tasks;
using uniqlo_project.Models;

namespace uniqlo_project.Services.GetUserInfoService
{
    public interface IGetUserInfoService
    {
        Task<GetUserInfoResponse> GetUserInfo();
    }
}
