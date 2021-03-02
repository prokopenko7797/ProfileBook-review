using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Constants;
using ProfileBook.Models;
using ProfileBook.Resources;
using ProfileBook.Servcies.Authorization;
using ProfileBook.Servcies.ProfileService;
using ProfileBook.Servcies.Settings;
using ProfileBook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {

        #region -----Private-----


        private readonly INavigationService _navigationService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        


        private ObservableCollection<Profile> _profileList;


        private string _tmp;
        private bool _IsVisible;
        

        #endregion

        public MainListViewModel(INavigationService navigationService, IProfileService profileService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            _profileService = profileService;

            //Title = LocalizationResource.MainList;
            //Title = Resources["MainList"];
            //Title = "Main List";
            //xmlns:resources="clr-namespace:ProfileBook.Resources"
            // Title="{x:Static resources:LocalizationResource.MainList}">
            //Title = (this.BindingContext as MainListViewModel).Resources["TheResourceYouWant"];

        }


        #region -----Public Properties-----


        public ObservableCollection<Profile> ProfileList 
        {
            get { return _profileList; }
            set { SetProperty(ref _profileList, value); }
        }

        public string tmp
        {
            get { return _tmp; }
            set { SetProperty(ref _tmp, value); }
        }

        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetProperty(ref _IsVisible, value); }
        }

        #endregion


        #region _____Comdands______

        private ICommand _LogOutToolBarCommand; 
        private ICommand _SettingsToolBarCommand;
        private ICommand _AddEditButtonClicked;
        private ICommand _DeleteCommandTap;
        private ICommand _EditCommandTap;
        private ICommand _ImageCommandTap;

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



        #region -----Private Helpers-----


        private async void DeleteCommand(object sender)
        {
            Profile profile = sender as Profile;
            if (profile == null) return;
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Delete?",
                OkText = "Yes",
                CancelText = "No"
            });
            if (result)
            {
                await _profileService.Dalete(profile.id);
                UpdateCollection();
            }
        }


        private async void EditCommand(object sender)
        {
            Profile profile = sender as Profile;

            var p = new NavigationParameters();
            p.Add(nameof(Profile), profile);

            await _navigationService.NavigateAsync($"{nameof(AddEditProfile)}", p);
        }


        private async void ModalImageComand(object sender)
        {
            Profile profile = sender as Profile;

            var p = new NavigationParameters();
            p.Add(nameof(Profile.image_path), profile.image_path);
            await _navigationService.NavigateAsync($"{nameof(ProfileImage)}", p, true, true);
        }


        private async void NavigateAddEditProfileCommand()
        {
            await _navigationService.NavigateAsync($"{nameof(AddEditProfile)}");

        }


        private async void NavigateSettingsCommand()
        {
            await _navigationService.NavigateAsync($"{nameof(Settings)}");
            /////////////////////////////////////////////////////////////////

        }


        private async void NavigateLogOutToolBarCommand() 
        {
            _authorizationService.LogOut();
            await _navigationService.NavigateAsync($"/NavigationPage/{nameof(SignIn)}");
        }

        #endregion


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

    }
}
