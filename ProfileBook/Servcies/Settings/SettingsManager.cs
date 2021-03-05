using ProfileBook.Constants;
using Xamarin.Essentials;

namespace ProfileBook.Servcies.Settings
{
    public class SettingsManager: ISettingsManager
    {

        public int SortBy
        {
            get => Preferences.Get(nameof(SortBy), Constant.DefaultSort);
            set => Preferences.Set(nameof(SortBy), value);
        }

        public string Lang
        {
            get => Preferences.Get(nameof(Lang), Constant.DefaultLanguage);
            set => Preferences.Set(nameof(Lang), value);
        }

        public int Theme
        {
            get => Preferences.Get(nameof(Theme), Constant.DefaultTheme);
            set => Preferences.Set(nameof(Theme), value);
        }
    }
}
