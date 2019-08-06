using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParser
{
  public class Parse
  {
    [Fact]
    public void ShouldReturnConnectDataResponse()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x04, 0x00, 0x00, 0x00, 0xAC, 0xBC, 0xCC, 0xDC, 0x34, 0xF5 });

      response.ResponseCode0.Should().Be(0x00);
      response.ResponseCode1.Should().Be(0x00);
      response.Counter.Should().Be(4);
      response.Should().BeOfType<ConnectDataResponse>();
      ((ConnectDataResponse)response).ConnectionIdentifier0.Should().Be(0xAC);
      ((ConnectDataResponse)response).ConnectionIdentifier1.Should().Be(0xBC);
      ((ConnectDataResponse)response).ConnectionIdentifier2.Should().Be(0xCC);
      ((ConnectDataResponse)response).ConnectionIdentifier3.Should().Be(0xDC);
    }

    [Fact]
    public void ShouldReturnGetAllRoomsDataResponse()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x02, 0x01, 0x00, 0x00, 0x1A, 0x00, 0x04, 0x00, 0x00, 0x00, 0xAC, 0xBC, 0xCC, 0xDC, 0x04, 0x00, 0xAA, 0xBB, 0xCC, 0xDD, 0x11, 0x00, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x99, 0x88, 0x77, 0x66, 0x2F, 0x6F });

      response.ResponseCode0.Should().Be(0x00);
      response.ResponseCode1.Should().Be(0x00);
      response.Counter.Should().Be(4);
      response.Should().BeOfType<GetAllRoomsDataResponse>();
      ((GetAllRoomsDataResponse)response).ConnectionIdentifier0.Should().Be(0xAC);
      ((GetAllRoomsDataResponse)response).ConnectionIdentifier1.Should().Be(0xBC);
      ((GetAllRoomsDataResponse)response).ConnectionIdentifier2.Should().Be(0xCC);
      ((GetAllRoomsDataResponse)response).ConnectionIdentifier3.Should().Be(0xDC);
      ((GetAllRoomsDataResponse)response).RoomIdentifiers.Length.Should().Be(0x04);
      ((GetAllRoomsDataResponse)response).RoomIdentifiers[0].Should().Be(-573785174);
      ((GetAllRoomsDataResponse)response).RoomIdentifiers[1].Should().Be(857866257);
      ((GetAllRoomsDataResponse)response).RoomIdentifiers[2].Should().Be(2003195204);
      ((GetAllRoomsDataResponse)response).RoomIdentifiers[3].Should().Be(1719109785);
    }

    [Fact]
    public void ShouldReturnCommonResponseForGetAllRoomsDataResponseIfResponseCodeIsNotZero()
    {
      var parser = CreateResponseParser();

      var response = parser.Parse(new byte[] { 0x00, 0x02, 0x01, 0x00, 0x00, 0x04, 0x00, 0x03, 0x00, 0xFF, 0x02, 0x6D, 0xAB });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x02);
      response.Counter.Should().Be(3);
      response.Should().BeOfType<CommonErrorResponse>();
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

      var response = parser.Parse(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAC, 0xBC, 0xCC, 0xDC, 0xAf, 0xFE });

      response.ResponseCode0.Should().Be(0xFF);
      response.ResponseCode1.Should().Be(0x01);
    }

    private static ResponseParser CreateResponseParser()
    {
      var dataConverters = new List<IDataConverter> { new StringConverter(), new ByteConverter(), new Int32Converter(), new UInt16Converter(), new Int32ArrayConverter() };
      return new ResponseParser(new DataConverterDispatcher(dataConverters));
    }
  }
}