using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._StringConverter
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldReturnDataValueAndNextDataIndex()
    {
      var converter = new StringConverter();

      var (dataValue, nextDataIndex) = converter.ConvertBack(new byte[] {0x55, 0x06, 0x00, 0x62, 0x00, 0x63, 0x00, 0x64, 0x00 }, 1);

      dataValue.Should().Be("bcd");
      nextDataIndex.Should().Be(0x09);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataIndexIsOutOfRange()
    {
      var converter = new StringConverter();
      const int outOfRangeDataIndex = 5;

      var action = new Action(() => converter.ConvertBack(new byte[] {0x01, 0x02, 0x03}, outOfRangeDataIndex));

      action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ShouldThrowExceptionIfDataLengthIsLongerThanDataBytes()
    {
      var converter = new StringConverter();

      var action = new Action(() => converter.ConvertBack(new byte[] {0xDA, 0x06, 0x00, 0x64, 0x00}, 1));

      action.Should().Throw<ArgumentOutOfRangeException>();
    }
  }
}