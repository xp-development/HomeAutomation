using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._Int32Converter
{
  public class Convert
  {
    [Fact]
    public void ShouldReturnByteArray()
    {
      var converter = new Int32Converter();
      const int data = 0x11223344;

      var bytes = converter.Convert(data).ToArray();

      bytes.Should().HaveCount(4);
      bytes[0].Should().Be(0x44);
      bytes[1].Should().Be(0x33);
      bytes[2].Should().Be(0x22);
      bytes[3].Should().Be(0x11);
    }
  }
}