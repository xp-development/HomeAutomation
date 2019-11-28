using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Rooms
{
  [RequestType(2, 0, 0, 0)]
  public class CreateRoomRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (CreateRoomDataRequest)request;
      Room room;
      using (var context = new ServerDatabaseContext())
      {
        room = new Room {Description = dataRequest.Description};
        context.Rooms.Add(room);
        context.SaveChanges();
      }

      return new CreateRoomDataResponse { Identifier = room.Id, ClientObjectIdentifier = dataRequest.ClientObjectIdentifier };
    }
  }
}