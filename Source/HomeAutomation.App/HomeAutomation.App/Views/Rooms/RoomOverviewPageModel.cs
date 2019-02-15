using System.Collections.ObjectModel;
using System.Linq;
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
    private readonly IGetRoomDescriptionRequestBuilder _getRoomDescriptionRequestBuilder;

    public RoomOverviewPageModel(ICommunicator communicator, IGetAllRoomsRequestBuilder getAllRoomsRequestBuilder, IGetRoomDescriptionRequestBuilder getRoomDescriptionRequestBuilder)
    {
      _communicator = communicator;
      _getAllRoomsRequestBuilder = getAllRoomsRequestBuilder;
      _getRoomDescriptionRequestBuilder = getRoomDescriptionRequestBuilder;
    }

    public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();

    private void OnReceiveData(IResponse response)
    {
      if (response is GetAllRoomsResponse getAllRoomsResponse)
      {
        Rooms.Clear();
        foreach (var roomIdentifier in getAllRoomsResponse.RoomIdentifiers)
        {
          Rooms.Add(new RoomViewModel(roomIdentifier));
          _communicator.SendAsync(_getRoomDescriptionRequestBuilder.Build(roomIdentifier));
        }
      }

      if (response is GetRoomDescriptionResponse getRoomDescriptionResponse)
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
      _communicator.SendAsync(_getAllRoomsRequestBuilder.Build());
      return base.OnLoadedAsync(parameter);
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }
  }
}