using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Responses;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._Responses._ResponseExtensions
{
  public class IsNotConnectedResponse
  {
    [Theory]
    [InlineData(0xFF, 0x02, true)]
    [InlineData(0xFE, 0x02, false)]
    [InlineData(0xFF, 0x01, false)]
    public void Usage(byte responseCode0, byte responseCode1, bool isNotConnectedResponse)
    {
      var responseMock = new Mock<IResponse>();
      responseMock.Setup(x => x.ResponseCode0).Returns(responseCode0);
      responseMock.Setup(x => x.ResponseCode1).Returns(responseCode1);

      responseMock.Object.IsNotConnectedResponse().Should().Be(isNotConnectedResponse);
    }
  }
}