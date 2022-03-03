using System;
using System.IO;
using System.Net.Http;
using System.Windows.Input;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Services.LogoutService;
using uniqlo_project.Services.UpdatePersonalDataService;
using uniqlo_project.Views;
using Newtonsoft.Json;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ILogoutService _logoutService;
        private readonly IUpdatePersonalDataService _pasServ;

        public SettingsPageViewModel(INavigationService navigationService, ILocalStorageService localStorageService, ILogoutService logoutService, IUpdatePersonalDataService pasServ)
            : base(navigationService)
        {
            _localStorageService = localStorageService;
            _logoutService = logoutService;
            _pasServ = pasServ;
        }

        public ICommand LogOutCommand => new Command(OnLogOutCommand);
        public ICommand ResCommand => new Command(OnResCommand);

        private async void OnResCommand(object obj)
        {
            //var res = await _pasServ.ResPas();
        }

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(OnBackCommand));

        private void OnBackCommand(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private async void OnLogOutCommand(object obj)
        {
            //var result = await _logoutService.SignOutAsync();

            //if (result.ToString() == "200")
            //{
                _localStorageService.Delete(Constants.IS_AUTOLOGIN_ALLOWED);
                _localStorageService.Delete(Constants.TOKEN_KEY);
                NavigationService.NavigateAsync(nameof(LoginPage));
            //}
        }
    }
}
