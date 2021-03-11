using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Servcies.Authorization;
using ProfileBook.Servcies.ProfileService;
using ProfileBook.Servcies.Settings;
using ProfileBook.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {

        #region _____Private______

        private ObservableCollection<Profile> _profileList;

        private ICommand _LogOutToolBarCommand;
        private ICommand _SettingsToolBarCommand;
        private ICommand _AddEditButtonClicked;
        private ICommand _DeleteCommandTap;
        private ICommand _EditCommandTap;
        private ICommand _ImageCommandTap;

        private bool _IsVisible;


        #endregion

        #region ______Services_____

        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;

        #endregion

        public MainListViewModel(INavigationService navigationService, ISettingsManager settingsManager, IProfileService profileService,
            IAuthorizationService authorizationService)
            : base(navigationService, settingsManager)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
        }


        #region _______Public Properties________


        public ObservableCollection<Profile> ProfileList 
        {
            get { return _profileList; }
            set { SetProperty(ref _profileList, value); }
        }



        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetProperty(ref _IsVisible, value); }
        }

        #endregion


        #region _____Comdands______

        public ICommand LogOutToolBarCommand =>
            _LogOutToolBarCommand ?? (_LogOutToolBarCommand =
            new Command(NavigateLogOutToolBarCommand));

        public ICommand SettingsToolBarCommand =>
            _SettingsToolBarCommand ?? (_SettingsToolBarCommand =
            new Command(NavigateSettingsCommand));

        public ICommand AddEditButtonClicked =>
            _AddEditButtonClicked ?? (_AddEditButtonClicked =
            new Command(NavigateAddEditProfileCommand));

        public ICommand EditCommandTap => _EditCommandTap ?? (_EditCommandTap = new Command(EditCommand));

        public ICommand DeleteCommandTap => _DeleteCommandTap ?? (_DeleteCommandTap = new Command(DeleteCommand));

        public ICommand ImageCommandTap => _ImageCommandTap ?? (_ImageCommandTap = new Command(ModalImageComand));

        #endregion



        #region _______Private Helpers_____


        private async void DeleteCommand(object sender)
        {
            if (!(sender is Profile profile)) return;

            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = Resources["Delete?"],
                OkText = Resources["Yes"],
                CancelText = Resources["No"]
            });
            if (result)
            {
                await _profileService.Dalete(profile.id);
                ProfileList.Remove(profile);
                if (ProfileList.Count() == 0) IsVisible = true;
            }
        }


        private async void EditCommand(object sender)
        {
            Profile profile = sender as Profile;

            var parametrs = new NavigationParameters
            {
                { nameof(Profile), profile }
            };

            await NavigationService.NavigateAsync($"{nameof(AddEditProfile)}", parametrs);
        }


        private async void ModalImageComand(object sender)
        {
            Profile profile = sender as Profile;

            var p = new NavigationParameters
            {
                { nameof(Profile.image_path), profile.image_path }
            };
            await NavigationService.NavigateAsync($"{nameof(ProfileImage)}", p, true, true);
        }


        private async void NavigateAddEditProfileCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(AddEditProfile)}");

        }


        private async void NavigateSettingsCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(Settings)}");
        }


        private async void NavigateLogOutToolBarCommand() 
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignIn)}");
        }

        #endregion

        #region ____Overrides_____

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            UpdateCollection();


        }

        private async void UpdateCollection()
        {
            ProfileList = new ObservableCollection<Profile>(await _profileService.GetUserSortedProfiles());



            if (ProfileList.Count() != 0) IsVisible = false;
            else IsVisible = true;
        }

        #endregion
    }
}
