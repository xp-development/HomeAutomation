using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Devices
{
  [RequestType(3, 0, 0, 0)]
  public class CreateDeviceRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (CreateDeviceDataRequest)request;
      Device device;
      using (var context = new ServerDatabaseContext())
      {
        device = new Device {Description = dataRequest.Description};
        context.Devices.Add(device);
        context.SaveChanges();
      }

      return new CreateDeviceDataResponse { Identifier = device.Id, ClientObjectIdentifier = dataRequest.ClientObjectIdentifier };
    }
  }
}