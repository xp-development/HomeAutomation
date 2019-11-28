using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Devices
{
  [RequestType(3, 2, 0, 0)]
  public class GetDeviceDescriptionRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (GetDeviceDescriptionDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var device = context.Devices.Find(dataRequest.Identifier);
        return device == null
          ? new GetDeviceDescriptionDataResponse {Description = "", Identifier = 0, ResponseCode0 = 0x01, ResponseCode1 = 0x00}
          : new GetDeviceDescriptionDataResponse {Description = device.Description, Identifier = device.Id};
      }
    }
  }
}