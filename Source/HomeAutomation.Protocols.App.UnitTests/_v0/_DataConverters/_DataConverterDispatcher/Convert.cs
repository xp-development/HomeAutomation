using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.DataConverters;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._DataConverters._DataConverterDispatcher
{
  public class Convert
  {
    [Fact]
    public void ShouldCallCorrectDataConverterIfDataTypeIsSameTypeAsDataType()
    {
      var converter = new Mock<IDataConverter>();
      converter.Setup(x => x.DataType).Returns(typeof(int));
      var dispatcher = new DataConverterDispatcher(new[] { converter.Object });

      dispatcher.Convert(11);

      converter.Verify(x => x.Convert(11), Times.Once);
    }

    [Fact]
    public void ShouldThrowExceptionIfDataTypeIsUnknown()
    {
      var converter = new Mock<IDataConverter>();
      converter.Setup(x => x.DataType).Returns(typeof(string));
      var dispatcher = new DataConverterDispatcher(new[] { converter.Object });

      var action = new Action(() => dispatcher.Convert(11));

      action.Should().Throw<Exception>();
    }
  }
}