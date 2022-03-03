using System;
using System.Threading.Tasks;
using uniqlo_project.Services.Api;

namespace uniqlo_project.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private IRestService _restService;

        public RegistrationService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<object> SignUpAsync(string login, string pass, string email, string first_name, string gender, string promo)
        {
            var source = "registration/";
            object requestBody = new { login, pass, email, first_name, gender, promo, app_key = Constants.APP_KEY };
            //var response = await _restService.PostAsync<Response>(source, requestBody);

            //
            //             MOCKING DATA start
            //

            var response = new Response { Code = "101" };

            //
            //             MOCKING DATA finish
            //
            return response.Code; //todo exceptions??
        }

        private class Response
        {
            public string Message { get; set; }
            public string Code { get; set; }
            public Data Data { get; set; }
        }

        private class Data
        {
            public string Email { get; set; }
        }
    }
}
