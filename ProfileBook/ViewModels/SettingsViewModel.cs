﻿using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Constants;
using ProfileBook.Enums;
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
        #region _____Services

        private readonly ISettingsManager _settingsManager;

        #endregion

        #region ____Private_____

        private DelegateCommand _SaveToolBarCommand;

        private OSAppTheme appTheme;

        private int _SelectedSort;
        private string _SelectedLang;
        private bool _IsChecked;
        private bool _IsCheckedEn;
        private bool _IsCheckedRu;
        private bool _IsCheckedName;
        private bool _IsCheckedNickName;
        private bool _IsCheckedDate;
        private int _Theme;
        private string _Lang;



        #endregion

        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager)
            : base(navigationService)
        {
            _settingsManager = settingsManager;


            Theme = (int)Application.Current.RequestedTheme;

        }



        #region ______Public Properties______

        public int SelectedSort
        {
            get { return _SelectedSort; }
            set { SetProperty(ref _SelectedSort, value); }
        }

        public string SelectedLang
        {
            get { return _SelectedLang; }
            set { SetProperty(ref _SelectedLang, value); }
        }

        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }

        public bool IsCheckedEn
        {
            get { return _IsCheckedEn; }
            set { SetProperty(ref _IsCheckedEn, value); }
        }

        public bool IsCheckedRu
        {
            get { return _IsCheckedRu; }
            set { SetProperty(ref _IsCheckedRu, value); }
        }

        public bool IsCheckedName
        {
            get { return _IsCheckedName; }
            set { SetProperty(ref _IsCheckedName, value); }
        }

        public bool IsCheckedNickName
        {
            get { return _IsCheckedNickName; }
            set { SetProperty(ref _IsCheckedNickName, value); }
        }

        public bool IsCheckedDate
        {
            get { return _IsCheckedDate; }
            set { SetProperty(ref _IsCheckedDate, value); }
        }

        public int Theme
        {
            get { return _Theme; }
            set { SetProperty(ref _Theme, value); }
        }

        public string Lang
        {
            get { return _Lang; }
            set { SetProperty(ref _Lang, value); }
        }

        #endregion


        #region _______Comands_______

        public DelegateCommand SaveToolBarCommand =>
           _SaveToolBarCommand ??
           (_SaveToolBarCommand = new DelegateCommand(SaveToolBar));

        #endregion

        #region _____Private Helpers______

        private async void SaveToolBar()
        {
            _settingsManager.SortBy = SelectedSort;

            _settingsManager.Theme = (int)appTheme;

            _settingsManager.Lang = SelectedLang;

            await NavigationService.GoBackAsync();
        }


        private void ChangeLang(string lang) 
        {
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(lang));
        }


        #endregion

        #region ________Overrides_________

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            SelectedSort = _settingsManager.SortBy;
            SelectedLang = _settingsManager.Lang;

            switch (_settingsManager.SortBy)
            {
                case (int)SortEnum.name:
                    IsCheckedName = true;
                    break;
                case (int)SortEnum.nick_name:
                    IsCheckedNickName = true; 
                    break;
                case (int)SortEnum.date:
                    IsCheckedDate = true;
                    break;
            }

            switch (_settingsManager.Lang)
            {
                case ResourcesLangConst.en:
                    IsCheckedEn = true;
                    break;
                case ResourcesLangConst.ru:
                    IsCheckedRu = true;
                    break;
            }



            Theme = _settingsManager.Theme;


            appTheme = (OSAppTheme)_settingsManager.Theme;
            Lang = _settingsManager.Lang;

            if (_settingsManager.Theme == (int)OSAppTheme.Unspecified)
                IsChecked = false;
            else IsChecked = true;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if(args.PropertyName == nameof(IsChecked))
            {
                if (IsChecked == true)
                {
                    ChangeLang(ResourcesLangConst.ru);

                    Lang = ResourcesLangConst.ru;

                    appTheme = OSAppTheme.Dark;
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                }
                else
                {
                    ChangeLang(ResourcesLangConst.en);

                    Lang = ResourcesLangConst.en;

                    appTheme = OSAppTheme.Unspecified;
                    Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                }
            }

            if (args.PropertyName == nameof(SelectedLang))
            {

                ChangeLang(SelectedLang);

                Lang = SelectedLang;
            }

        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            Application.Current.UserAppTheme = (OSAppTheme)_settingsManager.Theme;

            ChangeLang(_settingsManager.Lang);
        }

        #endregion

    }
}
