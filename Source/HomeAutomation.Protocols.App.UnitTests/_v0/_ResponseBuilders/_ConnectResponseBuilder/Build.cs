using FluentAssertions;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseBuilders._ConnectResponseBuilder
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var builder = new ConnectResponseBuilder();
      var connectRequest = new ConnectRequest(0, 11);

      var bytes = builder.Build(connectRequest);

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x01, "request type 0");
      bytes[2].Should().Be(0x00, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x0B, "counter");
      bytes[6].Should().Be(0x00, "counter");
      bytes[7].Should().Be(0x00, "response code 0");
      bytes[8].Should().Be(0x00, "response code 1");
      bytes[9].Should().Be(0x04, "data length");
      bytes[10].Should().Be(0x00, "data length");
      bytes[11].Should().Be(0x01, "connection identifier 0");
      bytes[12].Should().Be(0x00, "connection identifier 1");
      bytes[13].Should().Be(0x00, "connection identifier 2");
      bytes[14].Should().Be(0x00, "connection identifier 3");
      bytes[15].Should().Be(0x71, "crc");
      bytes[16].Should().Be(0x5C, "crc");
    }
  }
}