using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;

namespace uniqlo_project.Services.LocalStorageService
{
    public struct LocalStorageService : ILocalStorageService
    {
        private readonly ISettings _settings;

        public LocalStorageService(ISettings settings)
        {
            _settings = settings;
        }

        public string Load(string key)
        {
            var result = _settings.GetValueOrDefault(key, default(string));
            return result?.Replace("\"", string.Empty);
        }

        public void Save<T>(string key, T val)
        {
            _settings.AddOrUpdateValue(key, JsonConvert.SerializeObject(val));
        }

        public void Delete(string key)
        {
            _settings.Remove(key);
        }
    }
}
