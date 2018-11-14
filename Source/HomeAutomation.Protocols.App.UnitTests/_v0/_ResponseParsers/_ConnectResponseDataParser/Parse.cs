using FluentAssertions;
using HomeAutomation.Protocols.App.v0.ResponseParsers;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._ConnectResponseDataParser
{
  public class Parse
  {
    [Fact]
    public void ShouldSetIdentifierIfResponseCodeIsSuccess()
    {
      var parser = new ConnectResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x00, 0x00, new byte[0]);

      response.Identifier.Should().BeGreaterOrEqualTo(1);
    }

    [Fact]
    public void ShouldSetIdentifierTo0IfResponseCodeIsNotSuccess()
    {
      var parser = new ConnectResponseDataParser();

      var response = parser.Parse(0x00, 0x0001, 0x22, 0x11, new byte[0]);

      response.Identifier.Should().Be(0);
    }
  }
}