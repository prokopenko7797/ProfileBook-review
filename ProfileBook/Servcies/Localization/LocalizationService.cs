using ProfileBook.Constants;
using ProfileBook.Resources;
using ProfileBook.Servcies.Settings;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;

namespace ProfileBook.Servcies.Localization
{
    public class LocalizationService : ILocalizationService, INotifyPropertyChanged
    {
       

        readonly ResourceManager ResourceManager;

        private readonly ISettingsManager _settingsManager;


        CultureInfo CurrentCultureInfo;

        public string this[string key]
        {
            get
            {
                return ResourceManager.GetString(key, CurrentCultureInfo);
            }
        }


        public LocalizationService(ISettingsManager settingsManager) 
        {
            _settingsManager = settingsManager;
   

            CurrentCultureInfo = new CultureInfo( _settingsManager.Lang);
            ResourceManager = new ResourceManager(typeof(LocalizationResource));

            MessagingCenter.Subscribe<object, CultureInfo>(this,
                string.Empty, OnCultureChanged);
        }



        

        private void OnCultureChanged(object s, CultureInfo ci)
        {
            CurrentCultureInfo = ci;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

        public void CultureChange(string lang)
        {

            MessagingCenter.Send<object, CultureInfo>(this, string.Empty, new CultureInfo(lang));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
