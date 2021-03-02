using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class ProfileImageViewModel : ViewModelBase
    {

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { SetProperty(ref _ImagePath, value); }
        }

        public ProfileImageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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
