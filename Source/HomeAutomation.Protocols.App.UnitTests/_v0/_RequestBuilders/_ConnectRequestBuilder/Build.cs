using FluentAssertions;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestBuilders._ConnectRequestBuilder
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var counterMock = new Mock<ICounter>();
      counterMock.Setup(x => x.GetNext()).Returns(new byte[] {0x06, 0x07});
      var builder = new ConnectRequestBuilder(counterMock.Object);

      var bytes = builder.Build();

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x01, "request type 0");
      bytes[2].Should().Be(0x00, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x06, "counter");
      bytes[6].Should().Be(0x07, "counter");
      bytes[7].Should().Be(0x00, "data length");
      bytes[8].Should().Be(0x00, "data length");
      bytes[9].Should().Be(0x70, "crc");
      bytes[10].Should().Be(0x85, "crc");
    }
  }
}