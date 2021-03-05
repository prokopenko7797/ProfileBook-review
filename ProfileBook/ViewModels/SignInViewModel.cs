﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using ProfileBook.Models;
using ProfileBook.Servcies;
using ProfileBook.Servcies.Settings;
using ProfileBook.Servcies.Authorization;
using System.ComponentModel;
using Prism.Services;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {

        #region _____Services____

        private readonly IPageDialogService _pageDialogService;
        private readonly IAuthorizationService _authorization;

        #endregion

        #region ________Private______


        private string _Login;
        private string _Password;
        private bool _IsEnabled;


        private DelegateCommand _NavigateMainListCommand;
        private DelegateCommand _NavigateSignUpCommand;

        #endregion


        public SignInViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
        IAuthorizationService authorization): base(navigationService)
        {

            _pageDialogService = pageDialogService;
            _authorization = authorization;

        }


        #region -----Public Properties-----

        public string Login
        {
            get { return _Login; }
            set { SetProperty(ref _Login, value); }
        }

        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }


        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { SetProperty(ref _IsEnabled, value); }
        }

        #endregion


        #region _______Comands______

        public DelegateCommand NavigateMainListButtonTapCommand =>
            _NavigateMainListCommand ?? 
            (_NavigateMainListCommand = new DelegateCommand(ExecuteNavigateMainViewCommand)
                                                            .ObservesCanExecute(() => IsEnabled));


        public DelegateCommand NavigateSignUpButtonTapCommand =>
            _NavigateSignUpCommand ?? 
            (_NavigateSignUpCommand = new DelegateCommand(ExecuteNavigateSignUpCommand));

        #endregion 




        #region ________Private Helpers_______

        private async void ExecuteNavigateSignUpCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUp)}");

        }

        private async void ExecuteNavigateMainViewCommand()
        {
            if (await _authorization.Authorize(Login, Password))
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainList)}");

            else
                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["IncorrectLogPas"], Resources["Ok"]);
        }

        #endregion


        #region ________Overrides_______



        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.GetValue<string>("Login") != null)
                 Login = parameters.GetValue<string>("Login");

            Password = "";

        }



        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Login) || args.PropertyName == nameof(Password))
            {
                if (Login == null || Password == null) return;

                if (Login != "" && Password != "") IsEnabled = true;

                else if (Login != "" || Password == "") IsEnabled = false;
            }
        }



        #endregion

    }
}
