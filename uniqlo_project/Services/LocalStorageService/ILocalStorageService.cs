using System;
using System.Threading.Tasks;

namespace uniqlo_project.Services.LocalStorageService
{
    public interface ILocalStorageService
    {
        void Save<T>(string key, T val);
        void Delete(string key);
        string Load(string key);
    }
}
