using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using uniqlo_project.Resources.Strings;
using uniqlo_project.Services.Api;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Services.LoginService;
using uniqlo_project.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private ILoginService _loginService;
        private ILocalStorageService _localStorageService;

        public LoginPageViewModel(INavigationService navigationService, ILoginService loginService, ILocalStorageService localStorageService)
            : base(navigationService)
        {
            _loginService = loginService;
            _localStorageService = localStorageService;
        }

        private bool _isCheckedAutoLogin;
        public bool IsCheckedAutoLogin
        {
            get { return _isCheckedAutoLogin; }
            set { SetProperty(ref _isCheckedAutoLogin, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _emailAlert;
        public string EmailAlert
        {
            get { return _emailAlert; }
            set { SetProperty(ref _emailAlert, value); }
        }

        private string _passwordAlert;
        public string PasswordAlert
        {
            get { return _passwordAlert; }
            set { SetProperty(ref _passwordAlert, value); }
        }

        private bool _isVisblePasswordAlert = true;
        public bool IsVisblePasswordAlert
        {
            get { return _isVisblePasswordAlert; }
            set { SetProperty(ref _isVisblePasswordAlert, value); }
        }

        private bool _isValidPassword = true;
        public bool IsValidPassword
        {
            get { return _isValidPassword; }
            set { SetProperty(ref _isValidPassword, value); }
        }

        private bool _isValidEmail = true;
        public bool IsValidEmail
        {
            get { return _isValidEmail; }
            set { SetProperty(ref _isValidEmail, value); }
        }

        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ?? (_signInCommand = new Command(OnSignInCommandAsync));

        private ICommand _forgotPasswordCommand;
        public ICommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new Command(OnForgotPasswordCommandAsync));

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new Command(OnSignUpCommandAsync));


        public ICommand SelectCheckBoxCommand => new Command(OnSelectCheckBoxCommand);

        private void OnSelectCheckBoxCommand(object obj)
        {
            IsCheckedAutoLogin = !IsCheckedAutoLogin;
            if (IsCheckedAutoLogin)
            {
                _localStorageService.Save(Constants.IS_AUTOLOGIN_ALLOWED, IsCheckedAutoLogin);
            }
        }

        private void OnSignUpCommandAsync(object obj)
        {
            NavigationService.NavigateAsync(nameof(RegistrationPage));
        }

        private async void OnSignInCommandAsync(object obj)
        {
            //if (EmailValidation() & PasswordValidation())
            {
                var token = await _loginService.SignInAsync(Email, Password);
                if (token.ToString() == "409")
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, "Incorrect login or password", Strings.Ok);
                }
                else
                {
                    _localStorageService.Save(Constants.TOKEN_KEY, token);
                    await NavigationService.NavigateAsync(nameof(MainPageView));
                }
            }
        }

        private void OnForgotPasswordCommandAsync(object obj)
        {
            NavigationService.NavigateAsync(nameof(ForgotPassView));
        }

        private bool PasswordValidation()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordAlert = Strings.RequiredPassword;
                IsValidPassword = false;
            }
            else if (!Regex.IsMatch(Password, Constants.UserValidation.PASSWORD_PATTERN, RegexOptions.IgnoreCase))
            {
                PasswordAlert = Strings.InvalidPassword;
                IsValidPassword = false;
            }
            else
            {
                IsValidPassword = true;
                PasswordAlert = string.Empty;
            }

            return IsValidPassword;
        }

        private bool EmailValidation()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailAlert = Strings.RequiredEmail;
                IsValidEmail = false;
            }
            else if (!Regex.IsMatch(Email, Constants.UserValidation.EMAIL_PATTERN, RegexOptions.IgnoreCase))
            {
                EmailAlert = Strings.InvalidEmailAlert;
                IsValidEmail = false;
            }
            else
            {
                IsValidEmail = true;
                EmailAlert = string.Empty;
            }

            return IsValidEmail;
        }
    }
}
