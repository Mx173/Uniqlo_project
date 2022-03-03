using System;
using System.Threading.Tasks;
using uniqlo_project.Services.Api;
using uniqlo_project.Services.GetUserInfoService;
using uniqlo_project.Services.Json;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Services.LoginService;
using uniqlo_project.Services.LogoutService;
using uniqlo_project.Services.RegistrationService;
using uniqlo_project.Services.UpdatePersonalDataService;
using uniqlo_project.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace uniqlo_project
{
    public partial class App : PrismApplication
    {
        private ILocalStorageService _localStorageService;

        public App()
            : this(null)
        {
        }

        public App(Prism.IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            _localStorageService = Container.Resolve<ILocalStorageService>();

            InitializeComponent();

            MainPage = new NavigationPage(new BaseContentPage()) { };

            var isAllowedAutologin = _localStorageService.Load(Constants.IS_AUTOLOGIN_ALLOWED);

            if (isAllowedAutologin == "true")
            {
                await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(MainPageView));
            }
            else
            {
                await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<RegistrationPage>();
            containerRegistry.RegisterForNavigation<ForgotPassView>();
            containerRegistry.RegisterForNavigation<HomePageView>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<UserProfilePage>();
            containerRegistry.RegisterForNavigation<MainPageView>(); 
            containerRegistry.RegisterForNavigation<ChooseMarathonView>();
            containerRegistry.RegisterForNavigation<MenuView>();

            
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<ILocalStorageService>(Container.Resolve<LocalStorageService>());
            containerRegistry.RegisterInstance<IJsonService>(Container.Resolve<JsonService>());
            containerRegistry.RegisterInstance<IRestService>(Container.Resolve<RestService>());
            containerRegistry.RegisterInstance<ILoginService>(Container.Resolve<LoginService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<ILogoutService>(Container.Resolve<LogoutService>());
            containerRegistry.RegisterInstance<IGetUserInfoService>(Container.Resolve<GetUserInfoService>());
            containerRegistry.RegisterInstance<IUpdatePersonalDataService>(Container.Resolve<UpdatePersonalDataService>());
            
            //containerRegistry.RegisterInstance<ILocalStorageService>(Container.Resolve<LocalStorageService>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
