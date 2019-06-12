using System.Linq;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._StringConverter
{
  public class Convert
  {
    [Fact]
    public void ShouldReturnByteArray()
    {
      var converter = new StringConverter();
      const string data = "test";

      var bytes = converter.Convert(data).ToArray();

      const int dataLengthBytes = 2;
      const int unicode = 2;
      bytes.Should().HaveCount(dataLengthBytes + data.Length * unicode);
      bytes[0].Should().Be(0x08);
      bytes[1].Should().Be(0x00);
      bytes[2].Should().Be(0x74);
      bytes[3].Should().Be(0x00);
      bytes[4].Should().Be(0x65);
      bytes[5].Should().Be(0x00);
      bytes[6].Should().Be(0x73);
      bytes[7].Should().Be(0x00);
      bytes[8].Should().Be(0x74);
      bytes[9].Should().Be(0x00);
    }
  }
}