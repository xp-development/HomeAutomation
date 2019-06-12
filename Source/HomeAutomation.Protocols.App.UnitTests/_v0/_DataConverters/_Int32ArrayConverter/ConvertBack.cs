using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._Int32ArrayConverter
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldReturnDataValueAndNextDataIndex()
    {
      var converter = new Int32ArrayConverter();

      var (dataValue, nextDataIndex) = converter.ConvertBack(new byte[] {0xDD, 0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00 }, 1);

      var ints = (int[])dataValue;
      ints[0].Should().Be(0x00000001);
      ints[1].Should().Be(0x00000002);
      nextDataIndex.Should().Be(0x0B);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataIndexIsOutOfRange()
    {
      var converter = new Int32ArrayConverter();

      var action = new Action(() => converter.ConvertBack(new byte[] {0x01, 0x02, 0x03}, 1));

      action.Should().Throw<ArgumentException>();
    }
  }
}