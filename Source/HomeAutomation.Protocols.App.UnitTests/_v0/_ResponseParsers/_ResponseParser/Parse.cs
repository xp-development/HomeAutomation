using FluentAssertions;
using HomeAutomation.Protocols.App.v0.ResponseParsers;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._ResponseParser
{
  public class Parse
  {
    [Fact]
    public void ShouldReturnResponseWithResponseCodeForNotSupportedProtocolVersion()
    {
      var responseParserFactoryMock = new Mock<IResponseDataParserFactory>();
      var parser = new ResponseParser(responseParserFactoryMock.Object);

      var response = parser.Parse(new byte[] { 0x22, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x06);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D })]
    [InlineData(new byte[0])]
    public void ShouldReturnResponseWithResponseCodeForCorruptData(byte[] data)
    {
      var responseParserFactoryMock = new Mock<IResponseDataParserFactory>();
      var parser = new ResponseParser(responseParserFactoryMock.Object);

      var response = parser.Parse(data);

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x05);
    }

    [Fact]
    public void ShouldReturnResponseWithResponseCodeForUnknownCommand()
    {
      var responseParserFactoryMock = new Mock<IResponseDataParserFactory>();
      responseParserFactoryMock.Setup(x => x.TryCreate(0x01, 0x02, 0x03, 0x04)).Returns((IResponseDataParser) null);
      var parser = new ResponseParser(responseParserFactoryMock.Object);

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x04);
    }

    [Fact]
    public void ShouldReturnResponseWithResponseCodeForWrongCrc()
    {
      var responseParserFactoryMock = new Mock<IResponseDataParserFactory>();
      responseParserFactoryMock.Setup(x => x.TryCreate(0x01, 0x02, 0x03, 0x04)).Returns(new Mock<IResponseDataParser>().Object);
      var parser = new ResponseParser(responseParserFactoryMock.Object);

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x00, 0x00, 0x00, 0x00, 0x0D, 0x0E });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x01);
    }
  }
}