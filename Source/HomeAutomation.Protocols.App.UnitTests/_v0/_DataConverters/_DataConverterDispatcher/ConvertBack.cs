using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._DataConverterDispatcher
{
  public class ConvertBack
  {
    [Fact]
    public void ShouldCallCorrectDataConverterIfDataTypeIsSameTypeAsDataType()
    {
      var converter = new Mock<IDataConverter>();
      converter.Setup(x => x.DataType).Returns(typeof(int));
      converter.Setup(x => x.ConvertBack(It.IsAny<byte[]>(), It.IsAny<int>())).Returns((42, 4));
      var dispatcher = new DataConverterDispatcher(new[] { converter.Object });

      var (dataValue, dataIndex) = dispatcher.ConvertBack(typeof(int), new byte[] {0x2A, 0x00, 0x00, 0x00}, 0);

      converter.Verify(x => x.ConvertBack(new byte[] { 0x2A, 0x00, 0x00, 0x00 }, 0), Times.Once);
      dataValue.Should().Be(42);
      dataIndex.Should().Be(4);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataTypeIsUnknown()
    {
      var converter = new Mock<IDataConverter>();
      converter.Setup(x => x.DataType).Returns(typeof(string));
      var dispatcher = new DataConverterDispatcher(new[] { converter.Object });

      var action = new Action(() => dispatcher.ConvertBack(typeof(int), new byte[] { 0x2A, 0x00, 0x00, 0x00}, 0));

      action.Should().Throw<Exception>();
    }
  }
}