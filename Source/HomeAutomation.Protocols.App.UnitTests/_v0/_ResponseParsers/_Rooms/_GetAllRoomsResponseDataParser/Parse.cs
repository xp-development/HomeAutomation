using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.ResponseParsers;
using HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._Rooms._GetAllRoomsResponseDataParser
{
  public class Parse
  {
    [Fact]
    public void ShouldSetRoomIdentifierArrayWithOneRoom()
    {
      var parser = new GetAllRoomsResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x00, 0x00, new byte[] { 0x0A, 0x00, 0x00, 0x00 });

      response.RoomIdentifiers.Length.Should().Be(1);
      response.RoomIdentifiers[0].Should().Be(BitConverter.ToInt32(new byte[] { 0x0A, 0x00, 0x00, 0x00 }, 0));
    }

    [Fact]
    public void ShouldSetRoomIdentifierArrayWithTwoRooms()
    {
      var parser = new GetAllRoomsResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x00, 0x00, new byte[] { 0x0A, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00 });

      response.RoomIdentifiers.Length.Should().Be(2);
      response.RoomIdentifiers[0].Should().Be(BitConverter.ToInt32(new byte[] { 0x0A, 0x00, 0x00, 0x00 }, 0));
      response.RoomIdentifiers[1].Should().Be(BitConverter.ToInt32(new byte[] { 0x0B, 0x00, 0x00, 0x00 }, 0));
    }

    [Fact]
    public void ShouldSetRoomIdentifierArrayWithoutRooms()
    {
      var parser = new GetAllRoomsResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x00, 0x00, new byte[0]);

      response.RoomIdentifiers.Length.Should().Be(0);
    }
  }
}