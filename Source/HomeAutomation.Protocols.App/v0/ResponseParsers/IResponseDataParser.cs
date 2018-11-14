using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IResponseDataParser
  {
    IResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data);
  }
}