using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using ProfileBook.Enums;
using ProfileBook.Servcies.Registration;
using ProfileBook.Views;
using ProfileBook.Constants;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {

        #region -----Private-----

        private readonly IPageDialogService _pageDialogService;
        private readonly IRegistrationService _registrationService;


        private string _login;
        private string _password;
        private string _confirmpassword;



        private DelegateCommand _AddUserButtonTapCommand;

        #endregion


        public SignUpViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
            IRegistrationService registrationService)
            : base(navigationService)
        {

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
                        Resources["Error"], $"{Resources["LogVal1"]} {Constant.MinLoginLength} " +
                        $"{Resources["LogVal2"]} {Constant.MaxLoginLength} {Resources["LogVal3"]}", Resources["Ok"]);
                    }
                    break;

                case ValidEnum.NotInRangePassword:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], $"{Resources["PasVal1"]} {Constant.MinPasswordLength} " +
                        $"{Resources["LogVal2"]} {Constant.MaxPasswordLength} {Resources["LogVal3"]}", Resources["Ok"]);
                    }
                    break;
                case ValidEnum.HasntMach:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["PasMis"], Resources["Ok"]);
                    }
                    break;
                case ValidEnum.HasntUpLowNum:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["UpLowNum"], Resources["Ok"]);
                    }
                    break;

                case ValidEnum.StartWithNum:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["StartNum"], Resources["Ok"]);
                    }
                    break;
                case ValidEnum.LoginExist:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["LogExist"], Resources["Ok"]);
                    }
                    break;
                case ValidEnum.Success:
                    {

                        var p = new NavigationParameters
                        {
                            { "Login", Login }
                        };

                        await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignIn)}", p);
                    }
                    break;
                default:
                    {
                        await _pageDialogService.DisplayAlertAsync(
                            Resources["Error"], Resources["UnknownError"], Resources["Ok"]);
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
