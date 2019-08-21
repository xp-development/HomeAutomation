using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Rooms
{
  [RequestType(2, 3, 0, 0)]
  public class RenameRoomDescriptionRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (RenameRoomDescriptionDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var room = context.Rooms.Find(dataRequest.Identifier);
        if (room == null)
          return new RenameRoomDescriptionDataResponse {RoomIdentifier = dataRequest.Identifier, ResponseCode0 = 0x01, ResponseCode1 = 0x00};

        room.Description = dataRequest.Description;
        context.SaveChanges();
        return new RenameRoomDescriptionDataResponse {RoomIdentifier = room.Id};
      }
    }
  }
}