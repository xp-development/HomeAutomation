using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Devices
{
  [RequestType(3, 4, 0, 0)]
  public class DeleteDeviceRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (DeleteDeviceDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var device = context.Devices.Find(dataRequest.Identifier);
        if (device == null)
          return new DeleteDeviceDataResponse {Identifier = 0, ResponseCode0 = 0x01, ResponseCode1 = 0x00};

        context.Remove(device);
        context.SaveChanges();
        return new DeleteDeviceDataResponse {Identifier = device.Id};
      }
    }
  }
}