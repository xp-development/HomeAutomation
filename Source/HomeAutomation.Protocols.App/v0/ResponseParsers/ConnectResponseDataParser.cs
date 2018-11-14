using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class ConnectResponseDataParser : IResponseDataParser
  {
    private static uint _identifierCounter;

    public ConnectResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      uint identifier = 0;
      if (responseCode0 == 0x00 && responseCode1 == 0x00)
      {
        identifier = ++_identifierCounter;
      }

      return new ConnectResponse(protocolVersion, counter, responseCode0, responseCode1, identifier);
    }

    IResponse IResponseDataParser.Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}