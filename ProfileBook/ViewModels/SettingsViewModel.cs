using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Constants;
using ProfileBook.Localization;
using ProfileBook.Resources;
using ProfileBook.Servcies.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IUserDialogs _userDialogs;

        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _settingsManager = settingsManager;
            _userDialogs = userDialogs;



            Theme = (int)Application.Current.RequestedTheme;

        }

 

        private int _Selection;

        public int Selection
        {
            get { return _Selection; }
            set { SetProperty(ref _Selection, value); }
        }

        private bool _IsChecked;

        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }

        private int _Theme;

        public int Theme
        {
            get { return _Theme; }
            set { SetProperty(ref _Theme, value); }
        }

        private string _Lang;

        public string Lang
        {
            get { return _Lang; }
            set { SetProperty(ref _Lang, value); }
        }


        private DelegateCommand _SaveToolBarCommand;

        public DelegateCommand SaveToolBarCommand =>
           _SaveToolBarCommand ??
           (_SaveToolBarCommand = new DelegateCommand(SaveToolBar));


        private async void SaveToolBar()
        {
            _settingsManager.SortBy = Selection;

            if (IsChecked == true)
            {

                MessagingCenter.Send<object, CultureChangedMessage>(this,
               string.Empty, new CultureChangedMessage("ru"));
                //Resources = new LocalizedResources(typeof(LocalizationResource), "ru");


                _settingsManager.Theme = (int)OSAppTheme.Dark;
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else 
            {

                MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage("en-US"));

                //Resources = new LocalizedResources(typeof(LocalizationResource), "en-US");

                _settingsManager.Theme = (int)OSAppTheme.Unspecified;
                Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            }
            
            _settingsManager.Lang = Lang;

            await NavigationService.GoBackAsync();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            Selection = _settingsManager.SortBy;

            Theme = _settingsManager.Theme;

            Lang = _settingsManager.Lang;

            if (_settingsManager.Theme == (int)OSAppTheme.Unspecified)
                IsChecked = false;
            else IsChecked = true;
        }


    }
}
