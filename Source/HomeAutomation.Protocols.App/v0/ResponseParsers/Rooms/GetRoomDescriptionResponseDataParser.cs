using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms
{
  [ResponseDataParser(0x02, 0x02, 0x00, 0x00)]
  public class GetRoomDescriptionResponseDataParser : IResponseDataParser
  {
    public GetRoomDescriptionResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      var roomIdentifier = 0;
      var description = "";
      if (responseCode0 == 0x00 && responseCode1 == 0x00)
      {
        roomIdentifier = BitConverter.ToInt32(data.ToArray(), 0);
        description = Encoding.Unicode.GetString(data.Skip(4).ToArray());
      }

      return new GetRoomDescriptionResponse(protocolVersion, counter, responseCode0, responseCode1, roomIdentifier, description);
    }

    IResponse IResponseDataParser.Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}