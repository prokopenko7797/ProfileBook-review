using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Localization;
using ProfileBook.Resources;
using ProfileBook.Servcies.Settings;


namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected ISettingsManager _settingsManager { get; private set; }


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
