using System.Collections.Generic;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseParsers._ResponseDataParserFactory
{
  [ResponseDataParser(0x01, 0x02, 0x03, 0x04)]
  public class ResponseDataParserForUnitTest : IResponseDataParser
  {
    public IResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return null;
    }
  }
}