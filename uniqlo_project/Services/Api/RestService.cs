using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using uniqlo_project.Services.Json;
using Newtonsoft.Json;

namespace uniqlo_project.Services.Api
{
    public class RestService : IRestService
    {
        private readonly IJsonService _jsonService;
        public string BaseUrl { get; private set; }

        public RestService(IJsonService jsonService)
        {
            _jsonService = jsonService;

            BaseUrl = Constants.BASE_URL;
        }

        public async Task<T> PostAsync<T>(string source, object requestBody)
        {
            var jsonString = _jsonService.SerializeObject(requestBody);

            HttpContent content = requestBody as HttpContent;
            if (requestBody is IEnumerable<KeyValuePair<string, string>>)
            {
                content = new FormUrlEncodedContent(requestBody as IEnumerable<KeyValuePair<string, string>>);
            }
            if (content == null)
            {
                content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync(GetRequestUrl(BaseUrl, source), content).ConfigureAwait(false))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        var str = await response.Content.ReadAsStringAsync();
                        var data = _jsonService.DeserializeStream<T>(json);
                        return data;
                    }
                }
            }
        }

        public async Task<T> PutAsync<T>(string source, object requestBody)
        {
            var jsonString = _jsonService.SerializeObject(requestBody);

            HttpContent content = requestBody as HttpContent;
            if (requestBody is IEnumerable<KeyValuePair<string, string>>)
            {
                content = new FormUrlEncodedContent(requestBody as IEnumerable<KeyValuePair<string, string>>);
            }
            if (content == null)
            {
                content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            using (var client = new HttpClient())
            {
                using (var response = await client.PutAsync(GetRequestUrl(BaseUrl, source), content).ConfigureAwait(false))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        var str = await response.Content.ReadAsStringAsync();
                        var data = _jsonService.DeserializeStream<T>(json);
                        return data;
                    }
                }
            }
        }

        public async Task<T> GetAsync<T>(string source)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(GetRequestUrl(BaseUrl, source)).ConfigureAwait(false))
                {
                    // ThrowIfNotSuccess(response);
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        var str = await response.Content.ReadAsStringAsync();
                        var data = _jsonService.DeserializeStream<T>(json);
                        return data;
                    }
                }
            }
        }

       

        internal string GetRequestUrl(string host, string resource)
        {
            return $"{host}{resource}";
        }

     
    }
}
