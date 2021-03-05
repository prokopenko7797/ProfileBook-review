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
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected ISettingsManager _settingsManager { get; private set; }



    private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }




        public LocalizedResources Resources
        {
            get;
            private set;
        }




        public ViewModelBase(INavigationService navigationService, ISettingsManager settingsManager)
        {
            NavigationService = navigationService;
            _settingsManager = settingsManager;
            Resources = new LocalizedResources(typeof(LocalizationResource), _settingsManager.Lang);
        }




        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }


    }
}
