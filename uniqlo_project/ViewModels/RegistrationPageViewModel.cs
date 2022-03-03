using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using uniqlo_project.Enums;
using uniqlo_project.Resources.Strings;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Services.LoginService;
using uniqlo_project.Services.RegistrationService;
using uniqlo_project.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService;
        private ILoginService _loginService;

        private ILocalStorageService _localStorageService;

        public RegistrationPageViewModel(INavigationService navigationService, IRegistrationService registrationService, ILoginService loginService,
            ILocalStorageService localStorageService)
        : base(navigationService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
            _localStorageService = localStorageService;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private EGender _gender;
        public EGender Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _promoCode;
        public string PromoCode
        {
            get { return _promoCode; }
            set { SetProperty(ref _promoCode, value); }
        }

        public ICommand BackCommand => new Command(OnBackCommand);
        public ICommand ContinueClickedCommand => new Command(OnContinueClickedCommandExecuteAsync);
        public ICommand MaleClickCommand => new Command(OnMaleClickCommand);
        public ICommand FemaleClickCommand => new Command(OnFemaleClickCommand);

        private void OnMaleClickCommand(object obj)
        {
            Gender = EGender.Male;
        }
        private void OnFemaleClickCommand(object obj)
        {
            Gender = EGender.Female;
        }
        private void OnBackCommand(object obj)
        {
            NavigationService.GoBackAsync();
        }

        #region validation
        private bool _isValidName;
        public bool IsValidName
        {
            get { return _isValidName; }
            set { SetProperty(ref _isValidName, value); }
        }

        private bool _isValidFirstName;
        public bool IsValidFirstName
        {
            get { return _isValidFirstName; }
            set { SetProperty(ref _isValidFirstName, value); }
        }

        private bool _isValidEmail;
        public bool IsValidEmail
        {
            get { return _isValidEmail; }
            set { SetProperty(ref _isValidEmail, value); }
        }

        private bool _isValidPassword;
        public bool IsValidPassword
        {
            get { return _isValidPassword; }
            set { SetProperty(ref _isValidPassword, value); }
        }

        private string _firstNameAlert;
        public string FirstNameAlert
        {
            get { return _firstNameAlert; }
            set { SetProperty(ref _firstNameAlert, value); }
        }

        private string _nameAlert;
        public string NameAlert
        {
            get { return _nameAlert; }
            set { SetProperty(ref _nameAlert, value); }
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

        #endregion

        private async void OnContinueClickedCommandExecuteAsync()
        {
            try
            {
                if (EmailValidation() & PasswordValidation() & NameValidation())
                {
                    var code = await _registrationService.SignUpAsync(Name, Password, Email, FirstName, Gender.ToString().ToLower(), PromoCode);

                    if ((string)code == "200")
                    {
                        var token = await _loginService.SignInAsync(Name, Password);
                        var parameters = new NavigationParameters
                {
                    { Constants.NavigationParameterKey, token }
                };
                        _localStorageService.Save(Constants.TOKEN_KEY, token);
                        await NavigationService.NavigateAsync(nameof(MainPageView), parameters);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, "Invalid email or login", Strings.Ok);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region valid
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

        private bool NameValidation()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameAlert = Strings.RequiredName;
                IsValidName = false;
            }
            else if (!Regex.IsMatch(Name, Constants.UserValidation.NAME_PATTERN, RegexOptions.IgnoreCase))
            {
                NameAlert = Strings.InvalidNameAlert;
                IsValidName = false;
            }
            else
            {
                IsValidName = true;
                NameAlert = string.Empty;
            }

            return IsValidName;
        }

        private bool FirstNameValidation()
        {
            if (!Regex.IsMatch(FirstName, Constants.UserValidation.NAME_PATTERN, RegexOptions.IgnoreCase))
            {
                FirstNameAlert = Strings.InvalidFirstNameAlert;
                IsValidFirstName = false;
            }
            else
            {
                IsValidFirstName = true;
                FirstNameAlert = string.Empty;
            }

            return IsValidFirstName;
        }
        #endregion
    }
}
