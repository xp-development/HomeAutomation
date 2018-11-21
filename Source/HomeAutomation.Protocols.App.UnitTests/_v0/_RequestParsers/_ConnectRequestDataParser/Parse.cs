using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestParsers._ConnectRequestDataParser
{
  public class Parse
  {
    [Fact]
    public void DataShouldAlwaysByEmpty()
    {
      var parser = new ConnectRequestDataParser();

      var request = parser.Parse(0x00, 0x0022, new byte[0]);

      request.ProtocolVersion.Should().Be(0x00);
      request.Counter.Should().Be(0x0022);
      request.RequestType0.Should().Be(0x01);
      request.RequestType1.Should().Be(0x00);
      request.RequestType2.Should().Be(0x00);
      request.RequestType3.Should().Be(0x00);
    }

    [Theory]
    [InlineData(new byte[] { 0x00 })]
    [InlineData(new byte[] { 0x11, 0x03 })]
    public void ShouldThrowExceptionIfDataIsNotEmpty(byte[] data)
    {
      var parser = new ConnectRequestDataParser();

      new Action(() => parser.Parse(0x00, 0x0022, data)).Should().Throw<ConnectRequestDataException>();
    }
  }
}