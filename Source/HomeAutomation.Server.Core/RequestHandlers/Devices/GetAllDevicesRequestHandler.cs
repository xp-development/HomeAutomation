using System.Linq;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Devices
{
  [RequestType(3, 1, 0, 0)]
  public class GetAllDevicesRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      using (var context = new ServerDatabaseContext())
      {
        return new GetAllDevicesDataResponse { Identifiers = context.Devices.Select(x => x.Id).ToArray() };
      }
    }
  }
}