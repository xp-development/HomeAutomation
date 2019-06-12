using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomOverviewPageModel : ViewModelBase
  {
    private readonly ICommunicator _communicator;

    public RoomOverviewPageModel(ICommunicator communicator)
    {
      _communicator = communicator;
    }

    public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();

    private void OnReceiveData(IResponse response)
    {
      if (response is GetAllRoomsDataResponse getAllRoomsResponse)
      {
        Rooms.Clear();
        foreach (var roomIdentifier in getAllRoomsResponse.RoomIdentifiers)
        {
          Rooms.Add(new RoomViewModel(roomIdentifier));
          _communicator.SendAsync(new GetRoomDescriptionDataRequest { Identifier = roomIdentifier});
        }
      }

      if (response is GetRoomDescriptionDataResponse getRoomDescriptionResponse)
      {
        var roomViewModel = Rooms.FirstOrDefault(x => x.Id == getRoomDescriptionResponse.RoomIdentifier);
        if (roomViewModel != null)
        {
          roomViewModel.Description = getRoomDescriptionResponse.Description;
        }
      }
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      _communicator.SendAsync(new GetAllRoomsDataRequest());

      return base.OnLoadedAsync(parameter);
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }
  }
}