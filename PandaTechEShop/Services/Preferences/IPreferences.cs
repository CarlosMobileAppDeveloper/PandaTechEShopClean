namespace PandaTechEShop.Services.Preferences
{
    public interface IPreferences
    {
        void SetString(string key, string value);

        string GetString(string key, string defaultValue);
    }
}
