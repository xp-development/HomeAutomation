using Xamarin.Essentials;

namespace HomeAutomation.App
{
  public class UserSettings : IUserSettings
  {
    public bool ContainsKey(string key)
    {
      return Preferences.ContainsKey(key);
    }

    public string GetString(string key)
    {
      return Preferences.Get(key, "");
    }

    public int GetInt32(string key)
    {
      return Preferences.Get(key, 43123);
    }

    public void Set(string key, string value)
    {
      Preferences.Set(key, value);
    }

    public void Set(string key, int value)
    {
      Preferences.Set(key, value);
    }
  }
}