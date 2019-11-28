using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Devices;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Devices._DeleteDeviceRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldDeleteDeviceForExistingEntry()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Devices.Add(new Device {Id = 1, Description = "Device 1"});
        context.Devices.Add(new Device {Id = 2, Description = "Device 2"});
        context.Devices.Add(new Device {Id = 300, Description = "Device 300"});
        context.SaveChanges();
      }
      var requestHandler = new DeleteDeviceRequestHandler();

      var response = (DeleteDeviceDataResponse) requestHandler.Handle(new DeleteDeviceDataRequest { Identifier = 2 });

      response.Identifier.Should().Be(2);
      response.ResponseCode0.Should().Be(0);
      response.ResponseCode1.Should().Be(0);
      using (var context = new ServerDatabaseContext())
      {
        var devices = context.Devices.ToList();
        devices.Should().HaveCount(2);
        devices.Find(x => x.Id == 2).Should().BeNull();
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
      var requestHandler = new DeleteDeviceRequestHandler();

      var response = (DeleteDeviceDataResponse) requestHandler.Handle(new DeleteDeviceDataRequest { Identifier = 99 });

      response.ResponseCode0.Should().Be(0x01);
      response.ResponseCode1.Should().Be(0x00);
      response.Identifier.Should().Be(0);
      using (var context = new ServerDatabaseContext())
      {
        var devices = context.Devices.ToList();
        devices.Should().HaveCount(3);
      }
    }
  }
}