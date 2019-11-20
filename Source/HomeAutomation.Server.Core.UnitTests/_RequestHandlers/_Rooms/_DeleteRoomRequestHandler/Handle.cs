using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Rooms;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Rooms._DeleteRoomRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldDeleteRoomDForExistingEntry()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Add(new Room {Id = 1, Description = "Room 1"});
        context.Rooms.Add(new Room {Id = 2, Description = "Room 2"});
        context.Rooms.Add(new Room {Id = 300, Description = "Room 300"});
        context.SaveChanges();
      }
      var requestHandler = new DeleteRoomRequestHandler();

      var response = (DeleteRoomDataResponse) requestHandler.Handle(new DeleteRoomDataRequest { RoomIdentifier = 2 });

      response.RoomIdentifier.Should().Be(2);
      response.ResponseCode0.Should().Be(0);
      response.ResponseCode1.Should().Be(0);
    }

    [Fact]
    public void ShouldReturnErrorResponseCodeIfIdIsUnknown()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Add(new Room {Id = 1, Description = "Room 1"});
        context.Rooms.Add(new Room {Id = 2, Description = "Room 2"});
        context.Rooms.Add(new Room {Id = 300, Description = "Room 300"});
        context.SaveChanges();
      }
      var requestHandler = new DeleteRoomRequestHandler();

      var response = (DeleteRoomDataResponse) requestHandler.Handle(new DeleteRoomDataRequest { RoomIdentifier = 99 });

      response.ResponseCode0.Should().Be(0x01);
      response.ResponseCode1.Should().Be(0x00);
      response.RoomIdentifier.Should().Be(0);
    }
  }
}