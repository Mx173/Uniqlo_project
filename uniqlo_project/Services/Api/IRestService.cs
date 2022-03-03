using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uniqlo_project.Services.Api
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(string source);
        Task<T> PostAsync<T>(string source, object requestBody);
        Task<T> PutAsync<T>(string source, object requestBody);
    }
}
