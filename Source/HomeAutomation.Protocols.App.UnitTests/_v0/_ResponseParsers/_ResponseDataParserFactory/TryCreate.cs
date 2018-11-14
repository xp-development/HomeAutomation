using FluentAssertions;
using HomeAutomation.Protocols.App.v0.ResponseParsers;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._ResponseDataParserFactory
{
  public class TryCreate
  {
    [Fact]
    public void Usage()
    {
      var factory = new ResponseDataParserFactory();
      factory.Register<ResponseDataParserForUnitTest>();

      var responseDataParser = factory.TryCreate(0x01, 0x02, 0x03, 0x04);

      responseDataParser.Should().NotBeNull();
    }

    [Fact]
    public void ShouldReturnNullIfNoResponseDataParserIsRegisteredForRequestType()
    {
      var factory = new ResponseDataParserFactory();

      var responseDataParser = factory.TryCreate(0x00, 0x00, 0x00, 0x00);

      responseDataParser.Should().BeNull();
    }
  }
}