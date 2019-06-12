using FluentAssertions;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Server.Core.RequestHandlers;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._RequestHandlers._ConnectRequestHandler
{
  public class Handle
  {
    [Fact]
    public void ShouldReturnConnectDataResponseWithUniqueIdentifier()
    {
      var handler = CreateConnectRequestHandler();

      var response = handler.Handle(new ConnectDataRequest());

      var connectDataResponse = response.Should().BeOfType<ConnectDataResponse>().Subject;
      connectDataResponse.ConnectionIdentifier0.Should().Be(0x01);
      connectDataResponse.ConnectionIdentifier1.Should().Be(0x00);
      connectDataResponse.ConnectionIdentifier2.Should().Be(0x00);
      connectDataResponse.ConnectionIdentifier3.Should().Be(0x00);
    }

    [Fact]
    public void ShouldReturnConnectDataResponseWithIncrementedUniqueIdentifier()
    {
      var handler = CreateConnectRequestHandler();

      handler.Handle(new ConnectDataRequest());
      var response2 = handler.Handle(new ConnectDataRequest());

      var connectDataResponse = response2.Should().BeOfType<ConnectDataResponse>().Subject;
      connectDataResponse.ConnectionIdentifier0.Should().Be(0x02);
      connectDataResponse.ConnectionIdentifier1.Should().Be(0x00);
      connectDataResponse.ConnectionIdentifier2.Should().Be(0x00);
      connectDataResponse.ConnectionIdentifier3.Should().Be(0x00);
    }

    private static ConnectRequestHandler CreateConnectRequestHandler()
    {
      var handler = new ConnectRequestHandler();
      ConnectRequestHandler.Reset();
      return handler;
    }
  }
}