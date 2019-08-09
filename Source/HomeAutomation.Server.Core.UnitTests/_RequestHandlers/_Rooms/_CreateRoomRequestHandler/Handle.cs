using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Rooms;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Rooms._CreateRoomRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldCreateNewRoom()
    {
      var requestHandler = new CreateRoomRequestHandler();

      var response = (CreateRoomDataResponse) requestHandler.Handle(new CreateRoomDataRequest { ClientRoomIdentifier = 0x54, Description = "New room description" });

      response.ClientRoomIdentifier.Should().Be(0x54);
      response.RoomIdentifier.Should().Be(0x01);
      using (var context = new ServerDatabaseContext())
      {
        var rooms = context.Rooms.ToList();
        rooms.Should().HaveCount(1);
        rooms[0].Description.Should().Be("New room description");
        rooms[0].Id.Should().Be(0x01);
      }
    } 
  }
}