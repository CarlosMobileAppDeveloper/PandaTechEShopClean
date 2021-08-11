using System;
namespace PandaTechEShop.Services.Preferences
{
    public interface IPreferences
    {
        void Set(string key, string value);
    }
}
