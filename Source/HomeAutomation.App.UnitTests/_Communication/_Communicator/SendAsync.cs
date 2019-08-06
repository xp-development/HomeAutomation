using System.Net;
using HomeAutomation.App.Communication;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Communication._Communicator
{
  public class SendAsync
  {
    private readonly Mock<IUserSettings> _userSettingsMock;
    private readonly Mock<ITcpClient> _tcpClientMock;
    private readonly Mock<IConnectionIdentification> _connectionIdentificationMock;
    private readonly Mock<IRequestBuilder> _requestBuilderMock;
    private readonly Mock<IResponseParser> _responseParserMock;

    public SendAsync()
    {
      _tcpClientMock = new Mock<ITcpClient>();
      _responseParserMock = new Mock<IResponseParser>();
      _responseParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Returns<byte[]>(dataBytes => new ResponseBase {ResponseCode0 = 0x00, ResponseCode1 = 0x00});
      _userSettingsMock = new Mock<IUserSettings>();
      _userSettingsMock.Setup(x => x.GetString("ServerIP")).Returns("12.34.56.78");
      _userSettingsMock.Setup(x => x.GetInt32("ServerPort")).Returns(1234);
      _connectionIdentificationMock = new Mock<IConnectionIdentification>();
      _connectionIdentificationMock.Setup(x => x.Current).Returns(new byte[] {0xAA, 0xBB, 0xCC, 0xDD});
      _requestBuilderMock = new Mock<IRequestBuilder>();
      _requestBuilderMock.Setup(x => x.Build(It.IsAny<IRequest>())).Returns<IRequest>((request) =>
        request is ConnectDataRequest ? new byte[] {0x11, 0x22} : new byte[] {0x33, 0x44});
    }

    [Fact]
    public async void ShouldConnectTcpClientIfNotConnected()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(false);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, _responseParserMock.Object, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.ConnectAsync(It.Is<IPAddress>(y => y.Equals(IPAddress.Parse("12.34.56.78"))), It.Is<int>(y => y == 1234)));
    }

    [Fact]
    public async void ShouldNotConnectTcpClientIfAlreadyConnected()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, _responseParserMock.Object, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.ConnectAsync(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async void ShouldWriteBytes()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, _responseParserMock.Object, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.WriteAsync(It.Is<byte[]>(y => y[0] == 0x11 && y[1] == 0x22 )));
    }

    [Fact]
    public async void ShouldIncrementCounter()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, _responseParserMock.Object, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());
      _requestBuilderMock.Verify(x => x.Build(It.Is<IRequest>(y => y.Counter == 1)));
      await communicator.SendAsync(new ConnectDataRequest());
      _requestBuilderMock.Verify(x => x.Build(It.Is<IRequest>(y => y.Counter == 2)));
    }

    [Fact]
    public async void ShouldSendConnectRequestIfConnectionRequired()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var requestCounter = 0;
      _responseParserMock.Setup(x => x.Parse(It.IsAny<byte[]>())).Returns<byte[]>(dataBytes => requestCounter++ == 0
        ? new ResponseBase {ResponseCode0 = 0xFF, ResponseCode1 = 0x02}
        : new ResponseBase {ResponseCode0 = 0x00, ResponseCode1 = 0x00});

      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, _responseParserMock.Object, _requestBuilderMock.Object);

      await communicator.SendAsync(new GetAllRoomsDataRequest());

      _tcpClientMock.Verify(x => x.WriteAsync(It.Is<byte[]>(y => y[0] == 0x11 && y[1] == 0x22 )));
      _tcpClientMock.Verify(x => x.WriteAsync(It.Is<byte[]>(y => y[0] == 0x33 && y[1] == 0x44 )));
    }
  }
}