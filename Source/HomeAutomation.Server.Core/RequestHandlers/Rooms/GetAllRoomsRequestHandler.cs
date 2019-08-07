using System.Linq;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Rooms
{
  [RequestType(2, 1, 0, 0)]
  public class GetAllRoomsRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      using (var context = new ServerDatabaseContext())
      {
        return new GetAllRoomsDataResponse { RoomIdentifiers = context.Rooms.Select(x => x.Id).ToArray() };
      }
    }
  }
}