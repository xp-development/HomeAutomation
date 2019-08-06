using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Responses;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._Responses._CommonErrorResponse
{
  public class Ctor
  {
    [Theory]
    [InlineData(CommonResponseCode.WrongCrc, 0xFF, 0x01)]
    [InlineData(CommonResponseCode.NotConnected, 0xFF, 0x02)]
    [InlineData(CommonResponseCode.TransactionRequired, 0xFF, 0x03)]
    [InlineData(CommonResponseCode.UnknownCommand, 0xFF, 0x04)]
    [InlineData(CommonResponseCode.CorruptData, 0xFF, 0x05)]
    [InlineData(CommonResponseCode.NotSupportedProtocolVersion, 0xFF, 0x06)]
    public void Usage(CommonResponseCode responseCode, byte responseCode0, byte responseCode1)
    {
      var commonErrorResponse = new CommonErrorResponse(responseCode, 0, 0, 0, 0, 0);

      commonErrorResponse.ResponseCode0.Should().Be(responseCode0);
      commonErrorResponse.ResponseCode1.Should().Be(responseCode1);
    }
  }
}