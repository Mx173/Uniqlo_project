using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using uniqlo_project.Resources.Strings;
using uniqlo_project.Services.UpdatePersonalDataService;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class ForgotPassViewModel : BaseViewModel
    {
        public ForgotPassViewModel(INavigationService navigationService, IUpdatePersonalDataService serv)
            : base(navigationService)
        {
            _serv = serv;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _key;
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        private string _newpass;
        public string NewPas
        {
            get { return _newpass; }
            set { SetProperty(ref _newpass, value); }
        }

        private bool _isValidEmail = true;
        public bool IsValidEmail
        {
            get { return _isValidEmail; }
            set { SetProperty(ref _isValidEmail, value); }
        }

        private string _emailAlert;
        public string EmailAlert
        {
            get { return _emailAlert; }
            set { SetProperty(ref _emailAlert, value); }
        }

        private ICommand _resetPasswordCommand;
        public ICommand ResetPasswordCommand => _resetPasswordCommand ?? (_resetPasswordCommand = new Command(OnResetPasswordCommandAsync));

        private ICommand _newPasswordCommand;
        public ICommand NewPasswordCommand => _newPasswordCommand ?? (_newPasswordCommand = new Command(OnNewPasswordCommand));

        private async void OnNewPasswordCommand(object obj)
        {
            var res = await _serv.NewPas(Email, Key, NewPas);
            await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, res.Message, Strings.Ok);
        }

        private ICommand _backCommand;
        private IUpdatePersonalDataService _serv;

        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(OnBackCommand));

        private void OnBackCommand(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private async void OnResetPasswordCommandAsync()
        {
            EmailValidation();

            var res = await _serv.ResPas(Email);
            //if (res.Code != "200")
            //{
                await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, res.Message, Strings.Ok);
            //}

        }

        private void EmailValidation()
        {
            if (string.IsNullOrEmpty(Email))
            {
                IsValidEmail = false;
                EmailAlert = Strings.RequiredEmail;
            }
            else if (!Regex.IsMatch(Email, Constants.UserValidation.EMAIL_PATTERN, RegexOptions.IgnoreCase))
            {
                IsValidEmail = false;
                EmailAlert = Strings.InvalidEmailAlert;
            }
            else
            {
                IsValidEmail = true;
                EmailAlert = string.Empty;
            }
        }
    }
}
