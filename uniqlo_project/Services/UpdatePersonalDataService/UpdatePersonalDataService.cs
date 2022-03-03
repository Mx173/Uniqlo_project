using System;
using System.Net.Http;
using System.Threading.Tasks;
using uniqlo_project.Models;
using uniqlo_project.Services.Api;
using uniqlo_project.Services.LocalStorageService;

namespace uniqlo_project.Services.UpdatePersonalDataService
{
    public class UpdatePersonalDataService : IUpdatePersonalDataService
    {
        private IRestService _restService;
        private ILocalStorageService _localStorageService;
        //                 MOCK
        private UpdateAvatarResponse _updAvatarResponse_Mock;
        private UpdatePersonalDataResponse _updDataResponse_Mock;

        public UpdatePersonalDataService(IRestService restService, ILocalStorageService localStorageService)
        {
            _restService = restService;
            _localStorageService = localStorageService;
            //           MOCK
            _updDataResponse_Mock = new UpdatePersonalDataResponse();
            {
                _updDataResponse_Mock.Code = "errosSuccessCode";
                _updDataResponse_Mock.Message = "errosSuccessMessage";
                _updDataResponse_Mock.Data = new UpdatePersonalDataResponse.DataInfo();
            }
            _updAvatarResponse_Mock = new UpdateAvatarResponse();
            {
                _updAvatarResponse_Mock.Code = "errosSuccessCode";
                _updAvatarResponse_Mock.Message = "errosSuccessMessage";
                _updAvatarResponse_Mock.Data = new UpdateAvatarResponse.DataInfo();
            }
        }

        public async Task<UpdatePersonalDataResponse> UpdatePersonalData(string first_name, string gender, string email, string phone, string birth_date)
        {
            var access_token = _localStorageService.Load(Constants.TOKEN_KEY);
            object requestBody = new { access_token, first_name, gender, email, phone, birth_date};
            var source = "user/update-personal-data/";
            //var response = await _restService.PutAsync<UpdatePersonalDataResponse>(source, requestBody);
            
            return _updDataResponse_Mock; //todo exceptions??
        }

        public async Task<UpdateAvatarResponse> UpdateProfilePhoto(ByteArrayContent profile_photo)
        {
            var access_token = _localStorageService.Load(Constants.TOKEN_KEY);
            object requestBody = new { access_token, profile_photo };
            var source = "user/update-profile-photo/";
            //
            //             MOCKING DATA
            //
            //var response = await _restService.PostAsync<UpdateAvatarResponse>(source, requestBody);
            return _updAvatarResponse_Mock; //todo exceptions??
        }

        public async Task<UpdateAvatarResponse> ChangePassword(string old_password, string new_password)
        {
            var access_token = _localStorageService.Load(Constants.TOKEN_KEY);
            object requestBody = new { access_token, old_password, new_password };
            var source = "user/my-account/change-password";
            //
            //             MOCKING DATA
            //
            //var response = await _restService.PutAsync<UpdateAvatarResponse>(source, requestBody);
            return _updAvatarResponse_Mock; //todo exceptions??
        }

        public async Task<UpdateAvatarResponse> ResPas(string loginn)
        {
            var source = "user/reset-password/get-key?app_key=" + Constants.APP_KEY + "&identity=" + loginn;
            //
            //             MOCKING DATA
            //
            //var response = await _restService.GetAsync<UpdateAvatarResponse>(source);
            return _updAvatarResponse_Mock; //todo exceptions??
        }

        public async Task<UpdateAvatarResponse> NewPas(string loginn, string keyy, string newpassword)
        {
            object requestBody = new { app_key = Constants.APP_KEY, key = keyy, login = loginn, new_password = newpassword};
            var source = "user/reset-password/set-password";
            //
            //             MOCKING DATA
            //
            //var response = await _restService.PutAsync<UpdateAvatarResponse>(source, requestBody);
            return _updAvatarResponse_Mock; //todo exceptions??
        }
    }
}
