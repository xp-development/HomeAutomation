using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public class ConnectRequestDataParser : IRequestDataParser
  {
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