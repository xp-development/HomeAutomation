using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._Int32ArrayConverter
{
  public class Convert
  {
    [Fact]
    public void ShouldReturnByteArray()
    {
      var converter = new Int32ArrayConverter();
      int[] data = { 0x11223344, 0x0A223344 };

      var bytes = converter.Convert(data).ToArray();

      bytes.Should().HaveCount(0x0A);
      bytes[0].Should().Be(0x02);
      bytes[1].Should().Be(0x00);
      bytes[2].Should().Be(0x44);
      bytes[3].Should().Be(0x33);
      bytes[4].Should().Be(0x22);
      bytes[5].Should().Be(0x11);
      bytes[6].Should().Be(0x44);
      bytes[7].Should().Be(0x33);
      bytes[8].Should().Be(0x22);
      bytes[9].Should().Be(0x0A);
    }
  }
}