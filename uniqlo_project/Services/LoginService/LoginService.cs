using System;
using System.Threading.Tasks;
using uniqlo_project.Services.Api;
using Newtonsoft.Json;

namespace uniqlo_project.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private IRestService _restService;

        public LoginService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<object> SignInAsync(string login, string pass)
        {
            var source = "login/";
            object requestBody = new { login, pass, app_key = Constants.APP_KEY };
            //var response = await _restService.PostAsync<Response>(source, requestBody);

            //
            //             MOCKING DATA start
            //

            var response = new Response
            {
                Data = new Data
                {
                    User = new User { Access_Token = "token" }
                }
            };

            //
            //             MOCKING DATA finish
            //

            if (response.Code == "409")
            {
                return response.Code;
            }
            else
            {
                return response.Data.User.Access_Token;
            }
        }

        private class Response
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public Data Data { get; set; }
        }

        private class Data
        {
            public User User { get; set; }
        }

        private class User
        {
            public string Access_Token { get; set; }
        }
    }
}
