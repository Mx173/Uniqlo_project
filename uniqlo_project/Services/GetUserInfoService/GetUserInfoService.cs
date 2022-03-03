using System;
using System.Threading.Tasks;
using uniqlo_project.Models;
using uniqlo_project.Services.Api;
using uniqlo_project.Services.LocalStorageService;

namespace uniqlo_project.Services.GetUserInfoService
{
    public class GetUserInfoService : IGetUserInfoService
    {
        private IRestService _restService;
        private ILocalStorageService _localStorageService;

        public GetUserInfoService(IRestService restService, ILocalStorageService localStorageService)
        {
            _restService = restService;
            _localStorageService = localStorageService;
        }

        public async Task<GetUserInfoResponse> GetUserInfo()
        {
            var access_token = _localStorageService.Load(Constants.TOKEN_KEY);
            var source = "user/my-account?access_token=" + access_token;
            //var response = await _restService.GetAsync<GetUserInfoResponse>(source);

            //
            //             MOCKING DATA start
            //
            var response = new GetUserInfoResponse
            {
                Data = new GetUserInfoResponse.DataInfo { Gender = "Female",
                                                          User_login = "Joan",
                                                          User_email = "Joan33@ss.com",
                                                          First_name = "Joana",
                                                          Birth_date = "15-05-1999",
                                                          Avatar_url = "https://cdn131.picsart.com/339710847013203.png"
                },
                Code = "errorSuccessCode",
                Message = "errorSuccessMessage"
            };
            //
            //             MOCKING DATA finish
            //

            return response; //todo exceptions??
        }
    }
}
