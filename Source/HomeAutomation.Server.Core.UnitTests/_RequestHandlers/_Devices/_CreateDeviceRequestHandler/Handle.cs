using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Devices;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Devices._CreateDeviceRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldCreateNewDevice()
    {
      var requestHandler = new CreateDeviceRequestHandler();

      var response = (CreateDeviceDataResponse) requestHandler.Handle(new CreateDeviceDataRequest { ClientObjectIdentifier = 0x54, Description = "New device description" });

      response.ClientObjectIdentifier.Should().Be(0x54);
      response.Identifier.Should().Be(0x01);
      using (var context = new ServerDatabaseContext())
      {
        var devices = context.Devices.ToList();
        devices.Should().HaveCount(1);
        devices[0].Description.Should().Be("New device description");
        devices[0].Id.Should().Be(0x01);
      }
    } 
  }
}