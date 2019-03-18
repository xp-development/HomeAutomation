using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseBuilders._CommonResponseCodeResponseBuilder
{
  public class Build
  {
    [Theory]
    [InlineData(CommonResponseCode.WrongCrc, 0xFF, 0x01, 0x2C, 0xA4)]
    [InlineData(CommonResponseCode.NotConnected, 0xFF, 0x02, 0xDC, 0xA4)]
    [InlineData(CommonResponseCode.TransactionRequired, 0xFF, 0x03, 0x8D, 0x64)]
    [InlineData(CommonResponseCode.UnknownCommand, 0xFF, 0x04, 0x3C, 0xA5)]
    [InlineData(CommonResponseCode.CorruptData, 0xFF, 0x05, 0x6D, 0x65)]
    public void ShouldAddCommonResponseCoded(CommonResponseCode commonResponseCode, byte responseCode0, byte responseCode1, byte crc0, byte crc1)
    {
      var builder = new CommonResponseCodeResponseBuilder();

      var responseBytes = builder.Build(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xAF, 0xFE }, commonResponseCode);

      responseBytes.Length.Should().Be(13);
      responseBytes[0].Should().Be(0x00);
      responseBytes[1].Should().Be(0x01);
      responseBytes[2].Should().Be(0x02);
      responseBytes[3].Should().Be(0x03);
      responseBytes[4].Should().Be(0x04);
      responseBytes[5].Should().Be(0x00);
      responseBytes[6].Should().Be(0x00);
      responseBytes[7].Should().Be(responseCode0);
      responseBytes[8].Should().Be(responseCode1);
      responseBytes[9].Should().Be(0x00);
      responseBytes[10].Should().Be(0x00);
      responseBytes[11].Should().Be(crc0);
      responseBytes[12].Should().Be(crc1);
    }
  }
}