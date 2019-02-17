using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Models
{
  public class RoomViewModel : NotifyPropertyChangedBase
  {
    private string _description;

    public RoomViewModel(int id)
    {
      Id = id;
    }

    public int Id { get; }

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