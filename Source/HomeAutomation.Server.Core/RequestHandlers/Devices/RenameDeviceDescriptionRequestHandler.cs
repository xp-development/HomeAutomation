using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;

namespace HomeAutomation.Server.Core.RequestHandlers.Devices
{
  [RequestType(4, 3, 0, 0)]
  public class RenameDeviceDescriptionRequestHandler : IRequestHandler
  {
    public IResponse Handle(IRequest request)
    {
      var dataRequest = (RenameDeviceDescriptionDataRequest) request;

      using (var context = new ServerDatabaseContext())
      {
        var device = context.Devices.Find(dataRequest.Identifier);
        if (device == null)
          return new RenameDeviceDescriptionDataResponse {Identifier = dataRequest.Identifier, ResponseCode0 = 0x01, ResponseCode1 = 0x00};

        device.Description = dataRequest.Description;
        context.SaveChanges();
        return new RenameDeviceDescriptionDataResponse {Identifier = device.Id};
      }
    }
  }
}