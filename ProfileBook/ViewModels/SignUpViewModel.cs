using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using ProfileBook.Servcies.Registration;
using ProfileBook.Views;
using ProfileBook.Constants;
using Xamarin.Forms;
using ProfileBook.Validators;
using System.Threading.Tasks;
using ProfileBook.Servcies.Settings;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region _______Services______

        private readonly IPageDialogService _pageDialogService;
        private readonly IRegistrationService _registrationService;

        #endregion

        #region _______Private_______

        private string _login;
        private string _password;
        private string _confirmpassword;



        private DelegateCommand _AddUserButtonTapCommand;

        #endregion


        public SignUpViewModel(INavigationService navigationService, ISettingsManager settingsManager, IPageDialogService pageDialogService, 
            IRegistrationService registrationService)
            : base(navigationService, settingsManager)
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

        #endregion


        #region ________Comands________
        public DelegateCommand AddUserButtonTapCommand => 
            _AddUserButtonTapCommand ?? (_AddUserButtonTapCommand =
            new DelegateCommand(ExecuteddUserButtonTapCommand)).ObservesCanExecute(() => IsEnabled);


        #endregion


        #region -----Private Helpers-----

        private async Task<bool> LogPassCheck(string login, string password, string confirmpassword) 
        {
            if (!Validator.InRange(login, Constant.MinLoginLength, Constant.MaxLoginLength))
            {
                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], $"{Resources["LogVal1"]} {Constant.MinLoginLength} " +
                        $"{Resources["LogVal2"]} {Constant.MaxLoginLength} {Resources["LogVal3"]}", Resources["Ok"]);
                return false;
            }

            if (!Validator.InRange(password, Constant.MinPasswordLength, Constant.MaxPasswordLength))
            {
                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], $"{Resources["PasVal1"]} {Constant.MinPasswordLength} " +
                        $"{Resources["LogVal2"]} {Constant.MaxPasswordLength} {Resources["LogVal3"]}", Resources["Ok"]);
                return false;
            }


            if (Validator.StartWithNumeral(login))
            {
                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["StartNum"], Resources["Ok"]);
                return false;
            }

            if (!Validator.HasUpLowNum(password))
            {
                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["UpLowNum"], Resources["Ok"]);
                return false;
            }

            if (!Validator.Match(password, confirmpassword))
            {

                await _pageDialogService.DisplayAlertAsync(
                        Resources["Error"], Resources["PasMis"], Resources["Ok"]);
                return false;
            }
            return true;

        }


        private async void ExecuteddUserButtonTapCommand()
        {
            if(await LogPassCheck(Login, Password, ConfirmPassword))
            {
                if (await _registrationService.Registrate(Login, Password))
                {
                    var p = new NavigationParameters{ { "Login", Login } };

                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignIn)}", p);

                }
                else await _pageDialogService.DisplayAlertAsync(Resources["Error"], Resources["LogExist"], Resources["Ok"]);
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
