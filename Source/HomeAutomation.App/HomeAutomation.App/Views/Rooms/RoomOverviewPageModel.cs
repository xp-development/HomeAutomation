using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms;
using HomeAutomation.Protocols.App.v0.ResponseParsers;
using HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomOverviewPageModel : ViewModelBase
  {
    private readonly ICommunicator _communicator;
    private readonly IGetAllRoomsRequestBuilder _getAllRoomsRequestBuilder;
    private ObservableCollection<RoomViewModel> _rooms = new ObservableCollection<RoomViewModel>();

    public RoomOverviewPageModel(ICommunicator communicator, IGetAllRoomsRequestBuilder getAllRoomsRequestBuilder)
    {
      _communicator = communicator;
      _getAllRoomsRequestBuilder = getAllRoomsRequestBuilder;
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

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }

    private void OnReceiveData(IResponse response)
    {
      if (response is GetAllRoomsResponse getAllRoomsResponse)
      {
        Rooms.Clear();
        foreach (var roomIdentifier in getAllRoomsResponse.RoomIdentifiers)
        {
          Rooms.Add(new RoomViewModel(roomIdentifier));
          //          _communicator.SendAsync(_getRoomDescriptionRequestBuilder.Build());
        }
      }

//      if (response is GetRoomDescriptionResponse getRoomDescriptionResponse)
//      {
//      }
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      _communicator.SendAsync(_getAllRoomsRequestBuilder.Build());
      return base.OnLoadedAsync(parameter);
    }
  }
}