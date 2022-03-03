using System;
using System.Threading.Tasks;
using System.Windows.Input;
using uniqlo_project.Models;
using uniqlo_project.Services.GetUserInfoService;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private ILocalStorageService _localStorageService;
        private IGetUserInfoService _getUserInfoService;

        public HomePageViewModel(INavigationService navigationService, ILocalStorageService localStorageService, IGetUserInfoService getUserInfoService)
            : base(navigationService)
        {
            _localStorageService = localStorageService;
            _getUserInfoService = getUserInfoService;


        }

        private string _token;
        public string Token
        {
            get { return _token; }
            set { SetProperty(ref _token, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private ImageSource _attachment;
        public ImageSource Attachment
        {
            get { return _attachment; }
            set { SetProperty(ref _attachment, value); }
        }
        
        private ICommand _settingsCommand;
        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand = new Command(OnSettingsCommand));
        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(OnBackCommand));

        private void OnBackCommand(object obj)
        {
            NavigationService.GoBackAsync();
        }

        public ICommand GoToUserProfile => new Command(OnGoToUserProfile);

        private void OnGoToUserProfile(object obj)
        {
            NavigationService.NavigateAsync(nameof(UserProfilePage));
        }

        private void OnSettingsCommand(object obj)
        {
            NavigationService.NavigateAsync(nameof(SettingsPage));
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Token = _localStorageService.Load(Constants.TOKEN_KEY);

            var info = await _getUserInfoService.GetUserInfo();
            if (info.Data.First_name != "")
            {
                Name = info.Data.First_name;
            }
            else
            {
                Name = info.Data.User_login;
            }
            Attachment = info.Data.Avatar_url;
            
            Constants.LOGIN = info.Data.User_login;
            Constants.AVATAR = Attachment;
        }
    }
}
