using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._ByteConverter
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldReturnDataValueAndNextDataIndex()
    {
      var converter = new ByteConverter();

      var (dataValue, nextDataIndex) = converter.ConvertBack(new byte[] {0x01, 0x02, 0x03}, 1);

      dataValue.Should().Be(0x02);
      nextDataIndex.Should().Be(0x02);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataIndexIsOutOfRange()
    {
      var converter = new ByteConverter();
      const int outOfRangeDataIndex = 5;

      var action = new Action(() => converter.ConvertBack(new byte[] {0x01, 0x02, 0x03}, outOfRangeDataIndex));

      action.Should().Throw<IndexOutOfRangeException>();
    }
  }
}