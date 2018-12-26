using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms
{
  public class GetAllRoomsResponseDataParser : IResponseDataParser
  {
    public GetAllRoomsResponse Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      var roomIdentifiers = new List<int>();
      if (responseCode0 == 0x00 && responseCode1 == 0x00)
      {
        var enumerable = data as byte[] ?? data.ToArray();
        var dataCount = enumerable.Count();

        while (dataCount > 0)
        {
          roomIdentifiers.Add(BitConverter.ToInt32(enumerable, 0));
          enumerable = enumerable.Skip(4).ToArray();
          dataCount -= 4;
        }
      }

      return new GetAllRoomsResponse(protocolVersion, counter, responseCode0, responseCode1, roomIdentifiers.ToArray());
    }

    IResponse IResponseDataParser.Parse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, IEnumerable<byte> data)
    {
      return Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}