using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
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
    private readonly IEventAggregator _eventAggregator;
    private RoomViewModel _room;

    public RoomDetailPageModel(ICommunicator communicator, IEventAggregator eventAggregator)
    {
      _communicator = communicator;
      _eventAggregator = eventAggregator;
      SaveRoomCommand = new DelegateCommand<object, object>(OnSaveRoom);
      DeleteRoomCommand = new DelegateCommand<object, object>(OnDeleteRoom);
    }

    public DelegateCommand<object, object> SaveRoomCommand { get; }
    public DelegateCommand<object, object> DeleteRoomCommand { get; }

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

    private Task OnDeleteRoom(object arg)
    {
      return _communicator.SendAsync(new DeleteRoomDataRequest {Identifier = Room.Id});
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
        Room = new RoomViewModel { Id = getRoomDescriptionDataResponse.Identifier, Description = getRoomDescriptionDataResponse.Description };
      else if (response is DeleteRoomDataResponse deleteRoomDataResponse && deleteRoomDataResponse.RoomIdentifier == Room?.Id)
        _eventAggregator.PublishAsync(new NavigationEvent(typeof(RoomOverviewPage)));
    }
  }
}