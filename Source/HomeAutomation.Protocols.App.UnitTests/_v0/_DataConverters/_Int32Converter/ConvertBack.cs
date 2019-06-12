using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._Int32Converter
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldReturnDataValueAndNextDataIndex()
    {
      var converter = new Int32Converter();

      var (dataValue, nextDataIndex) = converter.ConvertBack(new byte[] {0xDD, 0x2A, 0x00, 0x00, 0x00}, 1);

      dataValue.Should().Be(0x2A);
      nextDataIndex.Should().Be(0x05);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataIndexIsOutOfRange()
    {
      var converter = new Int32Converter();

      var action = new Action(() => converter.ConvertBack(new byte[] {0x01, 0x02, 0x03}, 1));

      action.Should().Throw<ArgumentException>();
    }
  }
}