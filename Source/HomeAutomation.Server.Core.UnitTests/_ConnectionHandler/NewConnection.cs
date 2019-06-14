using FluentAssertions;
using Xunit;

namespace HomeAutomation.Server.Core.UnitTests._ConnectionHandler
{
  public class NewConnection
  {
    [Fact]
    public void ShouldReturnNextConnectionIdentifier()
    {
      var connectionHandler = new ConnectionHandler();

      var connectionIdentification = connectionHandler.NewConnection();

      connectionIdentification[0].Should().Be(0x01);
      connectionIdentification[1].Should().Be(0x00);
      connectionIdentification[2].Should().Be(0x00);
      connectionIdentification[3].Should().Be(0x00);
    }

    [Fact]
    public void ShouldIncrementConnectionIdentifier()
    {
      var connectionHandler = new ConnectionHandler();
      connectionHandler.NewConnection();

      var connectionIdentification = connectionHandler.NewConnection();

      connectionIdentification[0].Should().Be(0x02);
      connectionIdentification[1].Should().Be(0x00);
      connectionIdentification[2].Should().Be(0x00);
      connectionIdentification[3].Should().Be(0x00);
    }
  }
}