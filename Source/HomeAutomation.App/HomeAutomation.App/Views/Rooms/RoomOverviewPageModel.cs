using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;

namespace HomeAutomation.App.Views.Rooms
{
  public class RoomOverviewPageModel : ObjectOverviewPageModelBase<RoomDetailPage, RoomViewModel, GetAllRoomsDataRequest, GetAllRoomsDataResponse, GetRoomDescriptionDataRequest, GetRoomDescriptionDataResponse, CreateRoomDataRequest, CreateRoomDataResponse>
  {
    public RoomOverviewPageModel(ICommunicator communicator, IEventAggregator eventAggregator) : base(communicator,
      eventAggregator)
    {
    }

    protected override string NewObjectDescription { get; } = "New room";
  }
}