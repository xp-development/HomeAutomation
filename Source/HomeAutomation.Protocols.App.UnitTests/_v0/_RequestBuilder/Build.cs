using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestBuilder
{
  public class Build
  {
    [Fact]
    public void ShouldBuildConnectRequest()
    {
      var builder = CreateRequestBuilder();

      var bytes = builder.Build(new ConnectDataRequest { Counter = 3 });

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x01, "request type 0");
      bytes[2].Should().Be(0x00, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x02, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x03, "counter");
      bytes[8].Should().Be(0x00, "counter");
      bytes[9].Should().Be(0xC0, "crc");
      bytes[10].Should().Be(0x84, "crc");
    }

    [Fact]
    public void ShouldBuildGetAllRoomsRequest()
    {
      var builder = CreateRequestBuilder();

      var bytes = builder.Build(new GetAllRoomsDataRequest { ConnectionIdentifier0 = 0xAA , ConnectionIdentifier1 = 0xBB, ConnectionIdentifier2 = 0xCC, ConnectionIdentifier3 = 0xDD, Counter = 3 });

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x02, "request type 0");
      bytes[2].Should().Be(0x01, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x06, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x03, "counter");
      bytes[8].Should().Be(0x00, "counter");
      bytes[9].Should().Be(0xAA, "connection identifier 0");
      bytes[10].Should().Be(0xBB, "connection identifier 1");
      bytes[11].Should().Be(0xCC, "connection identifier 2");
      bytes[12].Should().Be(0xDD, "connection identifier 3");
      bytes[13].Should().Be(0xB9, "crc");
      bytes[14].Should().Be(0x86, "crc");
    }

    [Fact]
    public void ShouldBuildDeleteRoomDataRequest()
    {
      var builder = CreateRequestBuilder();

      var bytes = builder.Build(new DeleteRoomDataRequest { Identifier = 287454020, ConnectionIdentifier0 = 0xAA, ConnectionIdentifier1 = 0xBB, ConnectionIdentifier2 = 0xCC, ConnectionIdentifier3 = 0xDD, Counter = 3 });

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x02, "request type 0");
      bytes[2].Should().Be(0x04, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x0A, "data length");
      bytes[6].Should().Be(0x00, "data length");
      bytes[7].Should().Be(0x03, "counter");
      bytes[8].Should().Be(0x00, "counter");
      bytes[9].Should().Be(0xAA, "connection identifier 0");
      bytes[10].Should().Be(0xBB, "connection identifier 1");
      bytes[11].Should().Be(0xCC, "connection identifier 2");
      bytes[12].Should().Be(0xDD, "connection identifier 3");
      bytes[13].Should().Be(0x44, "unique room identifier 0");
      bytes[14].Should().Be(0x33, "unique room identifier 1");
      bytes[15].Should().Be(0x22, "unique room identifier 2");
      bytes[16].Should().Be(0x11, "unique room identifier 3");
      bytes[17].Should().Be(0x2D, "crc");
      bytes[18].Should().Be(0xF5, "crc");
    }

    private static RequestBuilder CreateRequestBuilder()
    {
      return new RequestBuilder(new DataConverterDispatcher(new List<IDataConverter>
      {
        new ByteConverter(),
        new StringConverter(),
        new Int32Converter(),
        new UInt16Converter()
      }));
    }
  }
}