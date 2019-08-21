using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomDetailPageModel : ViewModelBase
  {
    private readonly ICommunicator _communicator;
    private RoomViewModel _room;

    public RoomDetailPageModel(ICommunicator communicator)
    {
      _communicator = communicator;
      SaveRoomCommand = new DelegateCommand<object, object>(OnSaveRoom);
    }

    public DelegateCommand<object, object> SaveRoomCommand { get; }

    public RoomViewModel Room
    {
      get => _room;
      set
      {
        _room = value;
        InvokePropertyChanged();
      }
    }

    private Task OnSaveRoom(object arg)
    {
      return _communicator.SendAsync(new RenameRoomDescriptionDataRequest {Identifier = Room.Id, Description = Room.Description});
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      return _communicator.SendAsync(new GetRoomDescriptionDataRequest {Identifier = (int) parameter});
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }

    private void OnReceiveData(IResponse response)
    {
      if (response is GetRoomDescriptionDataResponse getRoomDescriptionDataResponse)
        Room = new RoomViewModel(getRoomDescriptionDataResponse.RoomIdentifier)
          {Description = getRoomDescriptionDataResponse.Description};
    }
  }
}