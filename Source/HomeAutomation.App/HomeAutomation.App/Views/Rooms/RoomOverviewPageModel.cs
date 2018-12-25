using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomOverviewPageModel : ViewModelBase
  {
    private ObservableCollection<RoomViewModel> _rooms = new ObservableCollection<RoomViewModel>();

    public RoomOverviewPageModel(IEventAggregator eventAggregator)
      : base(eventAggregator)
    {
    }

    protected override Task OnLoadedAsync(object parameter)
    {
//      var tcpClient = new TcpClient();
//
//      var stream = tcpClient.GetStream();
//
//      stream.Write(data, 0, data.Length);
//      Int32 bytes = stream.Read(data, 0, data.Length);
//      responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

      Rooms.Add(new RoomViewModel(1, "Living room"));
      Rooms.Add(new RoomViewModel(2, "Kitchen"));

      return base.OnLoadedAsync(parameter);
    }

    public ObservableCollection<RoomViewModel> Rooms
    {
      get => _rooms;
      set
      {
        _rooms = value;
        InvokePropertyChanged();
      }
    }
  }
}