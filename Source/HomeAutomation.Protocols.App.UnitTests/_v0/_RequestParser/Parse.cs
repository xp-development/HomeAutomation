using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParser;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestParser
{
  public class Parse
  {
    [Theory]
    [InlineData(0x01)]
    [InlineData(0x02)]
    [InlineData(0xFF)]
    [InlineData(0xDE)]
    public void ShouldThrowExceptionIfProtocolVersionIsNotSupported(byte protocolVersion)
    {
      var parser = new RequestParser(new Mock<IRequestDataParserFactory>().Object);

      new Action(() => parser.Parse(new byte[]{ protocolVersion, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })).Should().Throw<NotSupportedProtocolException>().Which.ProtocolVersion.Should().Be(protocolVersion);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x03 })]
    public void ShouldThrowExceptionIfDataIsCorrupt(byte[] data)
    {
      var parser = new RequestParser(new Mock<IRequestDataParserFactory>().Object);

      new Action(() => parser.Parse(data)).Should().Throw<CorruptDataException>();
    }

    [Fact]
    public void ShouldThrowExceptionIfCommandIsUnknown()
    {
      var parser = new RequestParser(new Mock<IRequestDataParserFactory>().Object);

      new Action(() => parser.Parse(new byte[] { 0x00, 0x01, 0xAA, 0xBB, 0xCC, 0xDD, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })).Should().Throw<UnknownCommandException>();
    }

    [Fact]
    public void ShouldThrowExceptionIfCrcIsWrong()
    {
      var dataParserFactoryMock = new Mock<IRequestDataParserFactory>();
      var dataParserMock = new Mock<IRequestDataParser>();
      dataParserFactoryMock.Setup(x => x.TryCreate(It.IsAny<byte>(), It.IsAny<byte>(), It.IsAny<byte>(), It.IsAny<byte>())).Returns(dataParserMock.Object);
      var parser = new RequestParser(dataParserFactoryMock.Object);

      new Action(() => parser.Parse(new byte[] { 0x00, 0x01, 0xAA, 0xBB, 0xCC, 0xDD, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })).Should().Throw<WrongCrcException>();
    }
  }
}