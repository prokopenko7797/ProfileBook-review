using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using ProfileBook.Servcies.Repository;
using ProfileBook.Servcies.Settings;
using ProfileBook.Servcies.Authorization;
using ProfileBook.Models;
using System.IO;
using System.Threading.Tasks;
using ProfileBook.Servcies.Registration;
using Xamarin.Essentials;
using ProfileBook.Servcies.ProfileService;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProfileBook.Constants;

namespace ProfileBook
{
    public partial class App
    {

        #region -----Services----
        private ISettingsManager _SettingsManager;
        private ISettingsManager SettingsManager => 
            _SettingsManager ?? (_SettingsManager = Container.Resolve<ISettingsManager>());

        private IAuthorizationService _AuthorizationService;
        private IAuthorizationService AuthorizationService =>
            _AuthorizationService ?? (_AuthorizationService = Container.Resolve<IAuthorizationService>());



        #endregion

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = (OSAppTheme)SettingsManager.Theme;

            if (AuthorizationService.IsAuthorize()) 
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignIn)}");
            else await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainList)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            

            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve <SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());


            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossMedia.Current);


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfile, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<ProfileImage, ProfileImageViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
        }
    }
}
