using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Rooms
{
  [RequestType(2, 4, 0, 0)]
  public class DeleteRoomRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (DeleteRoomDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var room = context.Rooms.Find(dataRequest.Identifier);
        if (room == null)
          return new DeleteRoomDataResponse {RoomIdentifier = 0, ResponseCode0 = 0x01, ResponseCode1 = 0x00};

        context.Remove(room);
        context.SaveChanges();
        return new DeleteRoomDataResponse {RoomIdentifier = room.Id};
      }
    }
  }
}