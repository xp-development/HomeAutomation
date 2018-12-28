using FluentAssertions;
using HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._Rooms._GetRoomDescriptionResponseDataParser
{
  public class Parse
  {
    [Fact]
    public void ShouldGetRoomDescription()
    {
      var parser = new GetRoomDescriptionResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x00, 0x00, new byte[] { 0x74, 0x00, 0x65, 0x00, 0x73, 0x00, 0x74, 0x00 });

      response.Description.Should().Be("test");
    }

    [Fact]
    public void RoomDescriptionShouldBeEmptyIfResponseCodeIsUnknownRoomIdentifier()
    {
      var parser = new GetRoomDescriptionResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x01, 0x00, new byte[] { 0x74, 0x00, 0x65, 0x00, 0x73, 0x00, 0x74, 0x00 });

      response.Description.Should().BeEmpty();
    }
  }
}