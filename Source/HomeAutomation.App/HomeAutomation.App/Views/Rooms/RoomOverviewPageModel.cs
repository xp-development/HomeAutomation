using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomOverviewPageModel : ViewModelBase
  {
    private readonly ICommunicator _communicator;
    private readonly Dictionary<byte, string> _newRooms = new Dictionary<byte, string>();
    private byte _clientRoomIdentifierForNewRooms;

    public RoomOverviewPageModel(ICommunicator communicator)
    {
      _communicator = communicator;
      NewRoomCommand = new DelegateCommand<object, object>(OnNewRoom);
    }

    public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();
    public DelegateCommand<object, object> NewRoomCommand { get; }

    private Task OnNewRoom(object arg)
    {
      const string newRoomDescription = "New room";
      _newRooms.Add(++_clientRoomIdentifierForNewRooms, newRoomDescription);
      return _communicator.SendAsync(new CreateRoomDataRequest { ClientRoomIdentifier = _clientRoomIdentifierForNewRooms, Description = newRoomDescription });
    }

    private async void OnReceiveData(IResponse response)
    {
      if (response is GetAllRoomsDataResponse getAllRoomsResponse)
      {
        Rooms.Clear();
        foreach (var roomIdentifier in getAllRoomsResponse.RoomIdentifiers)
        {
          Rooms.Add(new RoomViewModel(roomIdentifier));
          await _communicator.SendAsync(new GetRoomDescriptionDataRequest { Identifier = roomIdentifier});
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

      if (response is CreateRoomDataResponse createRoomResponse)
      {
        if (_newRooms.TryGetValue(createRoomResponse.ClientRoomIdentifier, out var description))
        {
          Rooms.Add(new RoomViewModel(createRoomResponse.RoomIdentifier) { Description = description });
        }
      }
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      _communicator.SendAsync(new GetAllRoomsDataRequest());
      _newRooms.Clear();

      return base.OnLoadedAsync(parameter);
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }
  }
}