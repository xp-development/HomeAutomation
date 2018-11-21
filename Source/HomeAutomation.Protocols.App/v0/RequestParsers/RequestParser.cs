using System;
using System.Linq;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public class RequestParser
  {
    private readonly IRequestDataParserFactory _parserFactory;

    public RequestParser(IRequestDataParserFactory parserFactory)
    {
      _parserFactory = parserFactory;
    }

    public IRequest Parse(byte[] dataBytes)
    {
      if (dataBytes == null || dataBytes.Length < 11)
        throw new CorruptDataException();

      var protocolVersion = dataBytes[0];
      if (protocolVersion != 0x00)
        throw new NotSupportedProtocolException(protocolVersion);

      var requestType0 = dataBytes[1];
      var requestType1 = dataBytes[2];
      var requestType2 = dataBytes[3];
      var requestType3 = dataBytes[4];

      var requestDataParser = _parserFactory.TryCreate(requestType0, requestType1, requestType2, requestType3);
      if (requestDataParser == null)
        throw new UnknownCommandException();

      var counter = BitConverter.ToUInt16(dataBytes, 5);
      var dataLength = BitConverter.ToUInt16(dataBytes, 7);

      var data = dataBytes.Skip(9).Take(dataLength);

      var crc0 = dataBytes[9 + dataLength];
      var crc1 = dataBytes[10 + dataLength];

      var computeChecksum = Crc16.ComputeChecksum(dataBytes.Take(dataBytes.Length - 2));
      if (crc0 != computeChecksum[0] || crc1 != computeChecksum[1])
        throw new WrongCrcException();

      return requestDataParser.Parse(protocolVersion, counter, data);
    }
  }
}