using System.Collections.Generic;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public interface IRequestDataParser
  {
    byte RequestType0 { get; }
    byte RequestType1 { get; }
    byte RequestType2 { get; }
    byte RequestType3 { get; }

    IRequest Parse(byte protocolVersion, ushort counter, IEnumerable<byte> data);
  }
}