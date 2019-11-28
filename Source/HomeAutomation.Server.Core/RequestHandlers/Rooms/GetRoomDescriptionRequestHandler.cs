using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Rooms
{
  [RequestType(2, 2, 0, 0)]
  public class GetRoomDescriptionRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (GetRoomDescriptionDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var room = context.Rooms.Find(dataRequest.Identifier);
        return room == null
          ? new GetRoomDescriptionDataResponse {Description = "", Identifier = 0, ResponseCode0 = 0x01, ResponseCode1 = 0x00}
          : new GetRoomDescriptionDataResponse {Description = room.Description, Identifier = room.Id};
      }
    }
  }
}