using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Servcies.Localization;
using ProfileBook.Servcies.Settings;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class ProfileImageViewModel : ViewModelBase
    {

        private string _ImagePath;

        

        public ProfileImageViewModel(INavigationService navigationService, ILocalizationService localizationService)
            : base(navigationService, localizationService)
        {
        }


        public string ImagePath
        {
            get { return _ImagePath; }
            set { SetProperty(ref _ImagePath, value); }
        }


        public ICommand GoBackCommand => new Command(GoBack);

        private async void GoBack() 
        {
             await NavigationService.GoBackAsync();
        }



        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ImagePath = parameters.GetValue<string>($"{nameof(Profile.image_path)}");
        }

    }
}
