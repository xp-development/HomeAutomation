using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Models
{
  public class RoomViewModel : NotifyPropertyChangedBase
  {
    private string _description;
    private int _id;

    public RoomViewModel(int id, string description)
    {
      _id = id;
      _description = description;
    }

    public int Id
    {
      get => _id;
      set
      {
        _id = value;
        InvokePropertyChanged();
      }
    }

    public string Description
    {
      get => _description;
      set
      {
        _description = value;
        InvokePropertyChanged();
      }
    }
  }
}