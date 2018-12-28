using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Models
{
  public class RoomViewModel : NotifyPropertyChangedBase
  {
    private string _description;
    private int _id;

    public RoomViewModel(int id)
    {
      _id = id;
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