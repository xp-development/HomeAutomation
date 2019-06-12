using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParser
{
  public class Parse
  {
    [Fact]
    public void ShouldReturnConnectDataResponse()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0xAC, 0xBC, 0xCC, 0xDC, 0xC9, 0xC4 });

      response.ResponseCode0.Should().Be(0x00);
      response.ResponseCode1.Should().Be(0x00);
      response.Should().BeOfType<ConnectDataResponse>();
      ((ConnectDataResponse)response).ConnectionIdentifier0.Should().Be(0xAC);
      ((ConnectDataResponse)response).ConnectionIdentifier1.Should().Be(0xBC);
      ((ConnectDataResponse)response).ConnectionIdentifier2.Should().Be(0xCC);
      ((ConnectDataResponse)response).ConnectionIdentifier3.Should().Be(0xDC);
    }

    [Fact]
    public void ShouldReturnResponseWithResponseCodeForNotSupportedProtocolVersion()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x22, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x06);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B })]
    [InlineData(new byte[0])]
    public void ShouldReturnResponseWithResponseCodeForCorruptData(byte[] data)
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(data);

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x05);
    }

    [Fact]
    public void ShouldReturnResponseWithResponseCodeForUnknownCommand()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x04);
    }

    [Fact]
    public void ShouldReturnResponseWithResponseCodeForWrongCrc()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0xAC, 0xBC, 0xCC, 0xDC, 0xAf, 0xFE });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x01);
    }

    private static ResponseParser CreateResponseParser()
    {
      var dataConverters = new List<IDataConverter> { new StringConverter(), new ByteConverter(), new Int32Converter() };
      return new ResponseParser(new DataConverterDispatcher(dataConverters));
    }
  }
}