using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Rooms;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Rooms._RenameRoomDescriptionRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldUpdateRoomDescriptionForExistingEntry()
    {
      const string newRoomDescription = "New room";
      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Add(new Room {Id = 1, Description = "Room 1"});
        context.Rooms.Add(new Room {Id = 2, Description = "Room 2"});
        context.Rooms.Add(new Room {Id = 300, Description = "Room 300"});
        context.SaveChanges();
      }
      var requestHandler = new RenameRoomDescriptionRequestHandler();

      requestHandler.Handle(new RenameRoomDescriptionDataRequest { Identifier = 2, Description = newRoomDescription });

      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Find(2).Description.Should().Be(newRoomDescription);
      }
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
      var requestHandler = new RenameRoomDescriptionRequestHandler();

      var response = (RenameRoomDescriptionDataResponse)requestHandler.Handle(new RenameRoomDescriptionDataRequest { Identifier = 99, Description = "New room" });

      response.ResponseCode0.Should().Be(0x01);
      response.ResponseCode1.Should().Be(0x00);
      response.RoomIdentifier.Should().Be(99);
    }
  }
}