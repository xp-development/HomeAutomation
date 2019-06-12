using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseBuilder
{
  public class Build
  {
    [Fact]
    public void ShouldBuildConnectDataResponse()
    {
      var builder = CreateResponseBuilder();

      var bytes = builder.Build(new ConnectDataResponse { ConnectionIdentifier0 = 0x01 });

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x01, "request type 0");
      bytes[2].Should().Be(0x00, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x06, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x00, "response code 0");
      bytes[8].Should().Be(0x00, "response code 1");
      bytes[9].Should().Be(0x01, "connection identifier 0");
      bytes[10].Should().Be(0x00, "connection identifier 1");
      bytes[11].Should().Be(0x00, "connection identifier 2");
      bytes[12].Should().Be(0x00, "connection identifier 3");
      bytes[13].Should().Be(0x7C, "crc");
      bytes[14].Should().Be(0x15, "crc");
    }

    [Fact]
    public void ShouldBuildCreateRoomDataResponse()
    {
      var builder = CreateResponseBuilder();

      var bytes = builder.Build(new CreateRoomDataResponse { ConnectionIdentifier0 = 0x01, ClientRoomIdentifier = 0x05, RoomIdentifier = 0x0000AABB });

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x02, "request type 0");
      bytes[2].Should().Be(0x00, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x0B, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x00, "response code 0");
      bytes[8].Should().Be(0x00, "response code 1");
      bytes[9].Should().Be(0x01, "connection identifier 0");
      bytes[10].Should().Be(0x00, "connection identifier 1");
      bytes[11].Should().Be(0x00, "connection identifier 2");
      bytes[12].Should().Be(0x00, "connection identifier 3");
      bytes[13].Should().Be(0x05, "client room identifier");
      bytes[14].Should().Be(0xBB, "room identifier 0");
      bytes[15].Should().Be(0xAA, "room identifier 1");
      bytes[16].Should().Be(0x00, "room identifier 2");
      bytes[17].Should().Be(0x00, "room identifier 3");
      bytes[18].Should().Be(0x7E, "crc");
      bytes[19].Should().Be(0xBF, "crc");
    }

    [Fact]
    public void ShouldBuildGetAllRoomsDataResponse()
    {
      var builder = CreateResponseBuilder();

      var bytes = builder.Build(new GetAllRoomsDataResponse { ConnectionIdentifier0 = 0x01, RoomIdentifiers = new[] { 0x00000001, 0x00554411, 0x0102030A }});

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x02, "request type 0");
      bytes[2].Should().Be(0x01, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x14, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x00, "response code 0");
      bytes[8].Should().Be(0x00, "response code 1");
      bytes[9].Should().Be(0x01, "connection identifier 0");
      bytes[10].Should().Be(0x00, "connection identifier 1");
      bytes[11].Should().Be(0x00, "connection identifier 2");
      bytes[12].Should().Be(0x00, "connection identifier 3");
      bytes[13].Should().Be(0x03, "room identifier length");
      bytes[14].Should().Be(0x00, "room identifier length");
      bytes[15].Should().Be(0x01, "room one identifier 0");
      bytes[16].Should().Be(0x00, "room one identifier 1");
      bytes[17].Should().Be(0x00, "room one identifier 2");
      bytes[18].Should().Be(0x00, "room one identifier 3");
      bytes[19].Should().Be(0x11, "room two identifier 0");
      bytes[20].Should().Be(0x44, "room two identifier 1");
      bytes[21].Should().Be(0x55, "room two identifier 2");
      bytes[22].Should().Be(0x00, "room two identifier 3");
      bytes[23].Should().Be(0x0A, "room three identifier 0");
      bytes[24].Should().Be(0x03, "room three identifier 1");
      bytes[25].Should().Be(0x02, "room three identifier 2");
      bytes[26].Should().Be(0x01, "room three identifier 3");
      bytes[27].Should().Be(0xA7, "crc");
      bytes[28].Should().Be(0x3D, "crc");
    }

    private static ResponseBuilder CreateResponseBuilder()
    {
      var builder = new ResponseBuilder(new DataConverterDispatcher(new List<IDataConverter>
      {
        new ByteConverter(),
        new StringConverter(),
        new Int32Converter(),
        new Int32ArrayConverter()
      }));
      return builder;
    }
  }
}