using System;
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
    }
}
