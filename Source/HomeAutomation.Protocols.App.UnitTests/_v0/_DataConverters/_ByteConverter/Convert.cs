using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._ByteConverter
{
  public class Convert
  {
    [Fact]
    public void ShouldReturnByteArray()
    {
      var converter = new ByteConverter();
      const byte data = 0x03;

      var bytes = converter.Convert(data).ToArray();

      bytes.Should().HaveCount(1);
      bytes[0].Should().Be(0x03);
    }
  }
}