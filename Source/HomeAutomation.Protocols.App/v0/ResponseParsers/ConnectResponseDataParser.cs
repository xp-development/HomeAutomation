using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  [ResponseDataParser(0x01, 0x00, 0x00, 0x00)]
  public class ConnectResponseDataParser : IResponseDataParser
  {
    public ConnectResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      uint identifier = 0;
      if (responseCode0 == 0x00 && responseCode1 == 0x00)
      {
        identifier = BitConverter.ToUInt32(data.ToArray(), 0);
      }

      return new ConnectResponse(protocolVersion, counter, responseCode0, responseCode1, identifier);
    }

    IResponse IResponseDataParser.Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}