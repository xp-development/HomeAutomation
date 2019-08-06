using System;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using Moq;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._HomeAutomationCommunication
{
  public class HandleReceivedBytes
  {
    private readonly Mock<IRequestParser> _requestParserMock;
    private readonly Mock<IResponseBuilder> _responseBuilderMock;
    private readonly Mock<IConnectionHandler> _connectionHandlerMock;
    private readonly Mock<IServiceLocator> _serviceLocatorMock;

    public HandleReceivedBytes()
    {
      _requestParserMock = new Mock<IRequestParser>();
      _responseBuilderMock = new Mock<IResponseBuilder>();
      _connectionHandlerMock = new Mock<IConnectionHandler>();
      _serviceLocatorMock = new Mock<IServiceLocator>();
    }

    [Fact]
    public void ShouldSetCounter()
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Returns(new GetAllRoomsDataRequest());
      _connectionHandlerMock.Setup(x => x.IsConnected(It.IsAny<byte[]>())).Returns(true);
      var communication = CreateHomeAutomationCommunication();

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x00});

      _responseBuilderMock.Verify(x => x.Build(It.Is<IResponse>(y => y.Counter == 8)));
    }

    [Theory]
    [InlineData(typeof(UnknownCommandException), CommonResponseCode.UnknownCommand)]
    [InlineData(typeof(WrongCrcException), CommonResponseCode.WrongCrc)]
    [InlineData(typeof(NotSupportedProtocolException), CommonResponseCode.NotSupportedProtocolVersion)]
    public void ShouldBuildResponseWithCommonResponseCode(Type exceptionType, CommonResponseCode expectedResponseCode)
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Throws((Exception) Activator.CreateInstance(exceptionType));
      var communication = CreateHomeAutomationCommunication();

      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x00});

      var bytes = BitConverter.GetBytes((ushort) expectedResponseCode);
      _responseBuilderMock.Verify(x => x.Build(It.Is<IResponse>(y => y.ResponseCode0 == bytes[1] && y.ResponseCode1 == bytes[0])));
    }

    [Fact]
    public void ShouldBuildResponseWithCommonResponseCodeNotConnected()
    {
      _requestParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Returns(new GetAllRoomsDataRequest());
      _connectionHandlerMock.Setup(x => x.IsConnected(It.IsAny<byte[]>())).Returns(false);
      var communication = CreateHomeAutomationCommunication();
      
      communication.HandleReceivedBytes(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x00});

      _responseBuilderMock.Verify(x => x.Build(It.Is<IResponse>(y => y.ResponseCode0 == 0xFF && y.ResponseCode1 == 0x02)));
    }

    private HomeAutomationCommunication CreateHomeAutomationCommunication()
    {
      return new HomeAutomationCommunication(_requestParserMock.Object, _responseBuilderMock.Object, _connectionHandlerMock.Object, _serviceLocatorMock.Object);
    }
  }
}