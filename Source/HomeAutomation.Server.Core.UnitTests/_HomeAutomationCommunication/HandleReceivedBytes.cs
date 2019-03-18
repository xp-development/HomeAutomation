using System;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
using Moq;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._HomeAutomationCommunication
{
  public class HandleReceivedBytes
  {
    [Theory]
    [InlineData(typeof(UnknownCommandException), CommonResponseCode.UnknownCommand)]
    [InlineData(typeof(WrongCrcException), CommonResponseCode.WrongCrc)]
    public void ShouldBuildResponseWithCommonResponseCode(Type exceptionType, CommonResponseCode expectedResponseCode)
    {
      var requestParserMock = new Mock<IRequestParser>();
      requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Throws((Exception) Activator.CreateInstance(exceptionType));
      var responseBuilderDispatcherMock = new Mock<IResponseBuilderDispatcher>();
      var commonResponseCodeResponseBuilder = new Mock<ICommonResponseCodeResponseBuilder>();
      var communication = new HomeAutomationCommunication(requestParserMock.Object, responseBuilderDispatcherMock.Object, commonResponseCodeResponseBuilder.Object);

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04});

      commonResponseCodeResponseBuilder.Verify(x => x.Build(It.IsAny<byte[]>(), It.Is<CommonResponseCode>(y => y == expectedResponseCode)));
    }

    [Fact]
    public void ShouldBuildResponseWithCommonResponseCodeNotSupportedProtocol()
    {
      var requestParserMock = new Mock<IRequestParser>();
      requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Throws(new NotSupportedProtocolException(0x01));
      var responseBuilderDispatcherMock = new Mock<IResponseBuilderDispatcher>();
      var commonResponseCodeResponseBuilder = new Mock<ICommonResponseCodeResponseBuilder>();
      var communication = new HomeAutomationCommunication(requestParserMock.Object, responseBuilderDispatcherMock.Object, commonResponseCodeResponseBuilder.Object);

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04});

      commonResponseCodeResponseBuilder.Verify(x => x.Build(It.IsAny<byte[]>(), It.Is<CommonResponseCode>(y => y == CommonResponseCode.NotSupportedProtocolVersion)));
    }
  }
}