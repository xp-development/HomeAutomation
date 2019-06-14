using FluentAssertions;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._ConnectionHandler
{
  public class IsConnected
  {
    [Fact]
    public void ShouldReturnTrueIfConnected()
    {
      var connectionHandler = new ConnectionHandler();
      var connectionIdentification = connectionHandler.NewConnection();

      var isConnected = connectionHandler.IsConnected(connectionIdentification);

      isConnected.Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnFalseIfNotConnected()
    {
      var connectionHandler = new ConnectionHandler();

      var isConnected = connectionHandler.IsConnected(new byte[]{ 0xFF, 0xFF, 0xFF, 0xFF });

      isConnected.Should().BeFalse();
    }
  }
}