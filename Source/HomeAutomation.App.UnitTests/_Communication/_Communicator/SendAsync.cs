using System.Net;
using HomeAutomation.App.Communication;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
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

    public SendAsync()
    {
      _tcpClientMock = new Mock<ITcpClient>();
      _userSettingsMock = new Mock<IUserSettings>();
      _userSettingsMock.Setup(x => x.GetString("ServerIP")).Returns("12.34.56.78");
      _userSettingsMock.Setup(x => x.GetInt32("ServerPort")).Returns(1234);
      _connectionIdentificationMock = new Mock<IConnectionIdentification>();
      _connectionIdentificationMock.Setup(x => x.Current).Returns(new byte[] {0xAA, 0xBB, 0xCC, 0xDD});
      _requestBuilderMock = new Mock<IRequestBuilder>();
      _requestBuilderMock.Setup(x => x.Build(It.IsAny<IRequest>())).Returns(new byte[] { 0x11, 0x22 });
    }

    [Fact]
    public async void ShouldConnectTcpClientIfNotConnected()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(false);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, null, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.ConnectAsync(It.Is<IPAddress>(y => y.Equals(IPAddress.Parse("12.34.56.78"))), It.Is<int>(y => y == 1234)));
    }

    [Fact]
    public async void ShouldNotConnectTcpClientIfAlreadyConnected()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, null, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.ConnectAsync(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async void ShouldWriteBytes()
    {
      _tcpClientMock.Setup(x => x.Connected).Returns(true);
      var communicator = new Communicator(_connectionIdentificationMock.Object, _userSettingsMock.Object, _tcpClientMock.Object, null, _requestBuilderMock.Object);

      await communicator.SendAsync(new ConnectDataRequest());

      _tcpClientMock.Verify(x => x.WriteAsync(It.Is<byte[]>(y => y[0] == 0x11 && y[1] == 0x22 )));
    }
  }
}