using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public class ConnectRequestDataParser : IRequestDataParser
  {
    public byte RequestType0 => 0x01;
    public byte RequestType1 => 0x00;
    public byte RequestType2 => 0x00;
    public byte RequestType3 => 0x00;

    public IRequest Parse(byte protocolVersion, ushort counter, IEnumerable<byte> data)
    {
      if (data == null || data.Any())
      {
        throw new ConnectRequestDataException();
      }

      return new ConnectRequest(protocolVersion, counter);
    }
  }
}