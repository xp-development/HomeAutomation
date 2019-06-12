using System;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._HomeAutomationCommunication
{
  public class HandleReceivedBytes
  {
    private readonly Mock<IRequestParser> _requestParserMock;
    private readonly Mock<IResponseBuilder> _responseBuilderMock;
    private readonly Mock<IServiceLocator> _serviceLocatorMock;
    private readonly Mock<ICommonResponseCodeResponseBuilder> _commonResponseCodeResponseBuilderMock;

    public HandleReceivedBytes()
    {
      _requestParserMock = new Mock<IRequestParser>();
      _responseBuilderMock = new Mock<IResponseBuilder>();
      _serviceLocatorMock = new Mock<IServiceLocator>();
      _serviceLocatorMock.Setup(x => x.Locate(It.IsAny<Type>())).Callback<Type>(x => Activator.CreateInstance(x));
      _commonResponseCodeResponseBuilderMock = new Mock<ICommonResponseCodeResponseBuilder>();
    }

    [Theory]
    [InlineData(typeof(UnknownCommandException), CommonResponseCode.UnknownCommand)]
    [InlineData(typeof(WrongCrcException), CommonResponseCode.WrongCrc)]
    public void ShouldBuildResponseWithCommonResponseCode(Type exceptionType, CommonResponseCode expectedResponseCode)
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Throws((Exception) Activator.CreateInstance(exceptionType));
      var communication = CreateHomeAutomationCommunication();

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04});

      _commonResponseCodeResponseBuilderMock.Verify(x => x.Build(It.IsAny<byte[]>(), It.Is<CommonResponseCode>(y => y == expectedResponseCode)));
    }

    [Fact]
    public void ShouldBuildResponseWithCommonResponseCodeNotSupportedProtocol()
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Throws(new NotSupportedProtocolException(0x01));
      var communication = CreateHomeAutomationCommunication();

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04});

      _commonResponseCodeResponseBuilderMock.Verify(x => x.Build(It.IsAny<byte[]>(), It.Is<CommonResponseCode>(y => y == CommonResponseCode.NotSupportedProtocolVersion)));
    }

    [Fact]
    public void ShouldBuildResponseWithCommonResponseCodeNotConnected()
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Returns(new GetAllRoomsDataRequest());
      var communication = CreateHomeAutomationCommunication();

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04});

      _commonResponseCodeResponseBuilderMock.Verify(x => x.Build(It.IsAny<byte[]>(), It.Is<CommonResponseCode>(y => y == CommonResponseCode.NotConnected)));
    }

    private HomeAutomationCommunication CreateHomeAutomationCommunication()
    {
      return new HomeAutomationCommunication(_requestParserMock.Object, _responseBuilderMock.Object, _commonResponseCodeResponseBuilderMock.Object, _serviceLocatorMock.Object);
    }
  }
}