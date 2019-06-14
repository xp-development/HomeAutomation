using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Server.Core.RequestHandlers;
using Moq;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._ConnectRequestHandler
{
  public class Handle
  {
    private static Mock<IConnectionHandler> _connectionHandlerMock;

    [Fact]
    public void ShouldReturnConnectDataResponseWithUniqueIdentifier()
    {
      var handler = CreateConnectRequestHandler();
      _connectionHandlerMock.Setup(x => x.NewConnection()).Returns(new byte[] { 0x01, 0x02, 0x03, 0x04 });

      var response = handler.Handle(new ConnectDataRequest());

      var connectDataResponse = response.Should().BeOfType<ConnectDataResponse>().Subject;
      connectDataResponse.ConnectionIdentifier0.Should().Be(0x01);
      connectDataResponse.ConnectionIdentifier1.Should().Be(0x02);
      connectDataResponse.ConnectionIdentifier2.Should().Be(0x03);
      connectDataResponse.ConnectionIdentifier3.Should().Be(0x04);
    }

    private static ConnectRequestHandler CreateConnectRequestHandler()
    {
      _connectionHandlerMock = new Mock<IConnectionHandler>();
      return new ConnectRequestHandler(_connectionHandlerMock.Object);
    }
  }
}