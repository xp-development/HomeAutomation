using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms
{
  public class GetRoomDescriptionResponseDataParser : IResponseDataParser
  {
    public GetRoomDescriptionResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      var description = "";
      if (responseCode0 == 0x00 && responseCode1 == 0x00)
      {
        description = Encoding.Unicode.GetString(data.ToArray());
      }

      return new GetRoomDescriptionResponse(protocolVersion, counter, responseCode0, responseCode1, description);
    }

    IResponse IResponseDataParser.Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}