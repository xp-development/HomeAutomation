using System;
using System.Collections.Generic;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestParser
{
  public class Parse
  {
    [Fact]
    public void ShouldParseConnectDataRequest()
    {
      var requestParser = CreateRequestParser();

      var request = requestParser.Parse(new byte[] {0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xD1});

      request.Should().BeOfType<ConnectDataRequest>();
    }

    [Fact]
    public void ShouldParseCreateRoomDataRequest()
    {
      var requestParser = CreateRequestParser();

      var request = requestParser.Parse(new byte[] { 0x00, 0x02, 0x00, 0x00, 0x00, 0x1D, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0xBB, 0x16, 0x00, 0x6C, 0x00, 0x69, 0x00, 0x76, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x67, 0x00, 0x20, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x6F, 0x00, 0x6D, 0x00, 0x49, 0x39 });

      request.Should().BeOfType<CreateRoomDataRequest>();
      ((CreateRoomDataRequest) request).ConnectionIdentifier0.Should().Be(0xCC);
      ((CreateRoomDataRequest) request).ConnectionIdentifier1.Should().Be(0xCC);
      ((CreateRoomDataRequest) request).ConnectionIdentifier2.Should().Be(0xCC);
      ((CreateRoomDataRequest) request).ConnectionIdentifier3.Should().Be(0xCC);
      ((CreateRoomDataRequest) request).ClientRoomIdentifier.Should().Be(0xBB);
      ((CreateRoomDataRequest) request).Description.Should().Be("living room");
    }

    [Fact]
    public void ShouldParseDeleteRoomDataRequest()
    {
      var requestParser = CreateRequestParser();

      var request = requestParser.Parse(new byte[] { 0x00, 0x02, 0x04, 0x00, 0x00, 0x08, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x42, 0x01, 0x00, 0x00, 0xC8, 0xC1 });

      request.Should().BeOfType<DeleteRoomDataRequest>();
      ((DeleteRoomDataRequest) request).ConnectionIdentifier0.Should().Be(0xCC);
      ((DeleteRoomDataRequest) request).ConnectionIdentifier1.Should().Be(0xCC);
      ((DeleteRoomDataRequest) request).ConnectionIdentifier2.Should().Be(0xCC);
      ((DeleteRoomDataRequest) request).ConnectionIdentifier3.Should().Be(0xCC);
      ((DeleteRoomDataRequest) request).RoomIdentifier.Should().Be(0x00000142);
    }

    [Theory]
    [InlineData(0x01)]
    [InlineData(0x02)]
    [InlineData(0xFF)]
    [InlineData(0xDE)]
    public void ShouldThrowExceptionIfProtocolVersionIsNotSupported(byte protocolVersion)
    {
      var parser = CreateRequestParser();

      new Action(() => parser.Parse(new byte[] { protocolVersion, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })).Should().Throw<NotSupportedProtocolException>().Which.ProtocolVersion.Should().Be(protocolVersion);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x03 })]
    public void ShouldThrowExceptionIfDataIsCorrupt(byte[] data)
    {
      var parser = CreateRequestParser();

      new Action(() => parser.Parse(data)).Should().Throw<CorruptDataException>();
    }

    [Fact]
    public void ShouldThrowExceptionIfCommandIsUnknown()
    {
      var parser = CreateRequestParser();

      new Action(() => parser.Parse(new byte[] { 0x00, 0x01, 0xAA, 0xBB, 0xCC, 0xDD, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })).Should().Throw<UnknownCommandException>();
    }

    [Fact]
    public void ShouldThrowExceptionIfCrcIsWrong()
    {
      var parser = CreateRequestParser();

      new Action(() => parser.Parse(new byte[] { 0x00, 0x02, 0x04, 0x00, 0x00, 0x08, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x42, 0x01, 0x00, 0x00, 0xAF, 0xFE })).Should().Throw<WrongCrcException>();
    }

    private static RequestParser CreateRequestParser()
    {
      var dataConverters = new List<IDataConverter> {new StringConverter(), new ByteConverter(), new Int32Converter()};
      return new RequestParser(new DataConverterDispatcher(dataConverters));
    }
  }
}