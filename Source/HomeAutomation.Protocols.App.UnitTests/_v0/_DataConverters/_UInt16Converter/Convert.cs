using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._UInt16Converter
{
  public class Convert
  {
    [Fact]
    public void ShouldReturnByteArray()
    {
      var converter = new UInt16Converter();
      const ushort data = 3;

      var bytes = converter.Convert(data).ToArray();

      bytes.Should().HaveCount(2);
      bytes[0].Should().Be(0x03);
      bytes[1].Should().Be(0x00);
    }
  }
}