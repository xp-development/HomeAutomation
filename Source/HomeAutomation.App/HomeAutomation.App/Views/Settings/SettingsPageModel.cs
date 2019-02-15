using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Views.Settings
{
  public class SettingsPageModel : ViewModelBase
  {
    private readonly IUserSettings _userSettings;

    public string ServerIp
    {
      get => _userSettings.GetString("ServerIP");
      set
      {
        _userSettings.Set("ServerIP", value);
        InvokePropertyChanged();
      }
    }

    public int ServerPort
    {
      get => _userSettings.GetInt32("ServerPort");
      set
      {
        _userSettings.Set("ServerPort", value);
        InvokePropertyChanged();
      }
    }

    public SettingsPageModel(IUserSettings userSettings)
    {
      _userSettings = userSettings;
    }
  }
}