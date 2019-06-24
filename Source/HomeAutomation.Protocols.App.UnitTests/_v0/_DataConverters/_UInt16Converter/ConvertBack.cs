using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._UInt16Converter
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldReturnDataValueAndNextDataIndex()
    {
      var converter = new UInt16Converter();

      var (dataValue, nextDataIndex) = converter.ConvertBack(new byte[] {0x55, 0x03, 0x00}, 1);

      dataValue.Should().Be(3);
      nextDataIndex.Should().Be(0x03);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataIndexIsOutOfRange()
    {
      var converter = new UInt16Converter();

      var action = new Action(() => converter.ConvertBack(new byte[] {0x01, 0x02}, 1));

      action.Should().Throw<ArgumentException>();
    }
  }
}