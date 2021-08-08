using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace SoccerPong.Services
{
    public class ConfigService
    {
        readonly string LS_KEY = typeof(ConfigService).Assembly.GetName().Name;

        readonly ILocalStorageService _localStorage;

        public Config Config { get; set; } = new();

        public ConfigService(ILocalStorageService localStorage) =>
            _localStorage = localStorage;

        public ValueTask ResetDefaultsAsync()
        {
            Config = new();
            return _localStorage.RemoveItemAsync(LS_KEY);
        }

        public async ValueTask<bool> LoadFromLocalStorageAsync()
        {
            if (await _localStorage.ContainKeyAsync(LS_KEY))
            {
                Config = await _localStorage.GetItemAsync<Config>(LS_KEY);
                return true;
            }
            return false;
        }

        public ValueTask SaveToLocalStorageAsync() =>
             _localStorage.SetItemAsync<Config>(LS_KEY, Config);
    }
}