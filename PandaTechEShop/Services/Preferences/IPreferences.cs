namespace PandaTechEShop.Services.Preferences
{
    public interface IPreferences
    {
        void Set(string key, string value);
        void Set(string key, int value);

        string Get(string key, string defaultValue);
        int Get(string key, int defaultValue);
    }
}
