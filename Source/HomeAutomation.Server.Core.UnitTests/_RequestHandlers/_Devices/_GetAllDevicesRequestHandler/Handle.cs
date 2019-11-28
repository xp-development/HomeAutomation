using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Devices;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Devices._GetAllDevicesRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldReturnAllDeviceIds()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Devices.Add(new Device {Id = 1});
        context.Devices.Add(new Device {Id = 2});
        context.Devices.Add(new Device {Id = 300});
        context.SaveChanges();
      }
      var requestHandler = new GetAllDevicesRequestHandler();

      var response = (GetAllDevicesDataResponse) requestHandler.Handle(new GetAllDevicesDataRequest());

      response.Identifiers.Should().HaveCount(3);
      response.Identifiers[0].Should().Be(1);
      response.Identifiers[1].Should().Be(2);
      response.Identifiers[2].Should().Be(300);
    }
  }
}