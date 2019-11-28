using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using HomeAutomation.Server.Core.DataAccessLayer;
using HomeAutomation.Server.Core.RequestHandlers.Rooms;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._Rooms._GetAllRoomsRequestHandler
{
  public class Handle : DatabaseTestBase
  {
    [Fact]
    public void ShouldReturnAllRoomIds()
    {
      using (var context = new ServerDatabaseContext())
      {
        context.Rooms.Add(new Room {Id = 1});
        context.Rooms.Add(new Room {Id = 2});
        context.Rooms.Add(new Room {Id = 300});
        context.SaveChanges();
      }
      var requestHandler = new GetAllRoomsRequestHandler();

      var response = (GetAllRoomsDataResponse) requestHandler.Handle(new GetAllRoomsDataRequest());

      response.Identifiers.Should().HaveCount(3);
      response.Identifiers[0].Should().Be(1);
      response.Identifiers[1].Should().Be(2);
      response.Identifiers[2].Should().Be(300);
    }
  }
}