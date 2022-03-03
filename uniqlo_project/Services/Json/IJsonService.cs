using System;
using Newtonsoft.Json;

namespace uniqlo_project.Services.Json
{
    public interface IJsonService
    {
        string SerializeObject(object value);

        T DeserializeObject<T>(string value);

        T DeserializeStream<T>(JsonTextReader jsonTextReader);
    }
}
