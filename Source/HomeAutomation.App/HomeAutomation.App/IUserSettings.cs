namespace HomeAutomation.App
{
  public interface IUserSettings
  {
    bool ContainsKey(string key);
    string GetString(string key);
    int GetInt32(string key);
    void Set(string key, string value);
    void Set(string key, int value);
  }
}