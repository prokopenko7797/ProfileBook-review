using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Resources;
using ProfileBook.Servcies.Localization;
using ProfileBook.Servcies.Settings;


namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }



        public ILocalizationService Resources
        {
            get;
            private set;
        }




        public ViewModelBase(INavigationService navigationService, ILocalizationService localizationService)
        {
            NavigationService = navigationService;
            Resources = localizationService;
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
