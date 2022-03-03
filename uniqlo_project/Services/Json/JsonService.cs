using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace uniqlo_project.Services.Json
{
    public class JsonService : IJsonService
    {
        private JsonSerializer _serializer = new JsonSerializer();

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, converters: new JsonConverter[] { new IsoDateTimeConverter() });
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, converters: new JsonConverter[] { new IsoDateTimeConverter() });
        }

        public T DeserializeStream<T>(JsonTextReader jsonTextReader)
        {
            _serializer.Converters.Add(new IsoDateTimeConverter());
            return _serializer.Deserialize<T>(jsonTextReader);
        }
    }
}
