namespace PandaTechEShop.Services.Preferences
{
    public class Preferences : IPreferences
    {
        public Preferences()
        {
        }

        public void SetString(string key, string value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public string GetString(string key, string defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }
    }
}
