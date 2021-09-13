namespace PandaTechEShop.Services.Preferences
{
    public class Preferences : IPreferences
    {
        public Preferences()
        {
        }

        public void Set(string key, string value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public void Set(string key, int value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public string Get(string key, string defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }

        public int Get(string key, int defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }
    }
}
