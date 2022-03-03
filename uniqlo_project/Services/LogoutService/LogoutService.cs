using System;
using System.Threading.Tasks;
using uniqlo_project.Services.Api;
using uniqlo_project.Services.LocalStorageService;

namespace uniqlo_project.Services.LogoutService
{
    public class LogoutService : ILogoutService
    {
        private IRestService _restService;
        private ILocalStorageService _localStorageService;

        public LogoutService(IRestService restService, ILocalStorageService localStorageService)
        {
            _restService = restService;
            _localStorageService = localStorageService;
        }

        public async Task<string> SignOutAsync()
        {
            var source = "logout/";
            var access_token = _localStorageService.Load(Constants.TOKEN_KEY);
            object requestBody = new { access_token };
            //var response = await _restService.PostAsync<Response>(source, requestBody);

            //
            //             MOCKING DATA start
            //
            var response = new Response { Code = "101"};
            //
            //             MOCKING DATA finish
            //

            return response.Code; //todo exceptions??
        }

        private class Response
        {
            public string Message { get; set; }
            public string Code { get; set; }
            public object Data { get; set; }
        }
    }
}
