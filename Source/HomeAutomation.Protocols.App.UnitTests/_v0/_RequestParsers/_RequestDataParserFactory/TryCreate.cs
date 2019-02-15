using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestParsers._RequestDataParserFactory
{
  public class TryCreate
  {
    [Fact]
    public void Usage()
    {
      var factory = new RequestDataParserFactory(new List<IRequestDataParser>{ new ConnectRequestDataParser() });

      var requestDataParser = factory.TryCreate(1, 0, 0, 0);

      requestDataParser.Should().NotBeNull();
      requestDataParser.Should().BeOfType<ConnectRequestDataParser>();
    }

    [Fact]
    public void ShouldReturnNullIfTypeIsUnknown()
    {
      var factory = new RequestDataParserFactory(new List<IRequestDataParser>());

      var requestDataParser = factory.TryCreate(22, 22, 22, 22);

      requestDataParser.Should().BeNull();
    }
  }
}