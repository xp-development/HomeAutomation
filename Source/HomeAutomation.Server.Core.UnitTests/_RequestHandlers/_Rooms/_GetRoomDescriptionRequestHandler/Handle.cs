using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Rooms;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Rooms._GetRoomDescriptionRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldReturnRoomDescriptionForExistingEntry()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Add(new Room {Id = 1, Description = "Room 1"});
        context.Rooms.Add(new Room {Id = 2, Description = "Room 2"});
        context.Rooms.Add(new Room {Id = 300, Description = "Room 300"});
        context.SaveChanges();
      }
      var requestHandler = new GetRoomDescriptionRequestHandler();

      var response = (GetRoomDescriptionDataResponse) requestHandler.Handle(new GetRoomDescriptionDataRequest { Identifier = 2 });

      response.Identifier.Should().Be(2);
      response.Description.Should().Be("Room 2");
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
      var requestHandler = new GetRoomDescriptionRequestHandler();

      var response = (GetRoomDescriptionDataResponse) requestHandler.Handle(new GetRoomDescriptionDataRequest { Identifier = 99 });

      response.ResponseCode0.Should().Be(0x01);
      response.ResponseCode1.Should().Be(0x00);
      response.Identifier.Should().Be(0);
      response.Description.Should().Be("");
    }
  }
}