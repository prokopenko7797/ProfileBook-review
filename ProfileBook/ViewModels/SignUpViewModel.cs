using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using ProfileBook.Enums;
using ProfileBook.Servcies.Registration;
using ProfileBook.Views;
using ProfileBook.Constants;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {

        #region -----Private-----
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IRegistrationService _registrationService;


        private string _login;
        private string _password;
        private string _confirmpassword;
        private string _tmp;


        private DelegateCommand _AddUserButtonTapCommand;

        #endregion


        public SignUpViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
            IRegistrationService registrationService)
            : base(navigationService)
        {
            Title = "Users SignUp";

            
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _registrationService = registrationService;

        }




        #region -----Public Properties-----


        public string Login
        {
            get { return _login; }
            set{ SetProperty(ref _login, value); }
        }


        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value);}
        }


        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set { SetProperty(ref _confirmpassword, value); }
        }

        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { SetProperty(ref _IsEnabled, value); }
        }



        public string tmp
        {
            get { return _tmp; }
            set { SetProperty(ref _tmp, value); }
        }

        
        public DelegateCommand AddUserButtonTapCommand =>
            _AddUserButtonTapCommand ??
            (_AddUserButtonTapCommand = 
            new DelegateCommand(ExecuteddUserButtonTapCommand)).ObservesCanExecute(() => IsEnabled);



        #endregion


        #region -----Private Helpers-----

        private async void ExecuteddUserButtonTapCommand()
        {
            switch (await _registrationService.Registrate(Login, Password, ConfirmPassword))
            {
                case ValidEnum.NotInRangeLogin:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", $"Login must be at least {Constant.MinLoginLength} " +
                        $"and no more than {Constant.MaxLoginLength} characters.", "OK");
                    }
                    break;

                case ValidEnum.NotInRangePassword:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", $"Password must be at least {Constant.MinPasswordLength} " +
                        $"and no more than {Constant.MaxPasswordLength} characters.", "OK");
                    }
                    break;
                case ValidEnum.HasntMach:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", "Password mismatch.", "OK");
                    }
                    break;
                case ValidEnum.HasntUpLowNum:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", "Password must contain at least one uppercase letter, one lowercase letter and one number.", "OK");
                    }
                    break;

                case ValidEnum.StartWithNum:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", "Login should not start with number.", "OK");
                    }
                    break;
                case ValidEnum.LoginExist:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        "Error", "Login already exist.", "OK");
                    }
                    break;
                case ValidEnum.Success:
                    {

                        var p = new NavigationParameters();
                        p.Add("Login", Login);

                        await _navigationService.NavigateAsync($"/NavigationPage/{nameof(SignIn)}", p);
                    }
                    break;
                default:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                            "Error", "Unknown error", "OK");
                    }
                    break;

            }
         
            
        }



        #endregion


        #region -----Overrides-----
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Login) || args.PropertyName == nameof(Password) || args.PropertyName == nameof(ConfirmPassword))
            {
                if (Login == null || Password == null || ConfirmPassword == null) return;

                if (Login != "" && Password != "" && ConfirmPassword != "") IsEnabled = true;

                else if (Login == "" || Password == "" || ConfirmPassword == "") IsEnabled = false;
            }
        }

        #endregion

    }
}
