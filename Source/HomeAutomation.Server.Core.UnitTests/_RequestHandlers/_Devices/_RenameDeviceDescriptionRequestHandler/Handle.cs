using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Devices;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Devices._RenameDeviceDescriptionRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldUpdateDeviceDescriptionForExistingEntry()
    {
      const string newDeviceDescription = "New device";
      using (var context = new ServerDatabaseContext())
      {
        context.Devices.Add(new Device {Id = 1, Description = "Device 1"});
        context.Devices.Add(new Device {Id = 2, Description = "Device 2"});
        context.Devices.Add(new Device {Id = 300, Description = "Device 300"});
        context.SaveChanges();
      }
      var requestHandler = new RenameDeviceDescriptionRequestHandler();

      requestHandler.Handle(new RenameDeviceDescriptionDataRequest { Identifier = 2, Description = newDeviceDescription });

      using (var context = new ServerDatabaseContext())
      {
        context.Devices.Find(2).Description.Should().Be(newDeviceDescription);
      }
    }

    [Fact]
    public void ShouldReturnErrorResponseCodeIfIdIsUnknown()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Devices.Add(new Device {Id = 1, Description = "Device 1"});
        context.Devices.Add(new Device {Id = 2, Description = "Device 2"});
        context.Devices.Add(new Device {Id = 300, Description = "Device 300"});
        context.SaveChanges();
      }
      var requestHandler = new RenameDeviceDescriptionRequestHandler();

      var response = (RenameDeviceDescriptionDataResponse)requestHandler.Handle(new RenameDeviceDescriptionDataRequest { Identifier = 99, Description = "New device" });

      response.ResponseCode0.Should().Be(0x01);
      response.ResponseCode1.Should().Be(0x00);
      response.Identifier.Should().Be(99);
    }
  }
}