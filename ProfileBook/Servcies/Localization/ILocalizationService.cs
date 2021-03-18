using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Servcies.Localization
{
    public interface ILocalizationService
    {
        string this[string key]
        {
            get;
        }

        void CultureChange(string lang);
    }
}
