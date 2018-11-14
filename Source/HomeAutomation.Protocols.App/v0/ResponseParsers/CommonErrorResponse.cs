using System;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class CommonErrorResponse : IResponse
  {
    public CommonErrorResponse(byte[] dataBytes, byte responseCode0, byte responseCode1)
    {
      dataBytes = dataBytes ?? new byte[0];
      ProtocolVersion = dataBytes.ElementAtOrDefault(0);
      RequestType0 = dataBytes.ElementAtOrDefault(1);
      RequestType1 = dataBytes.ElementAtOrDefault(2);
      RequestType2 = dataBytes.ElementAtOrDefault(3);
      RequestType3 = dataBytes.ElementAtOrDefault(4);
      if (dataBytes.Length >= 7)
        Counter = BitConverter.ToUInt16(dataBytes, 5);

      ResponseCode0 = responseCode0;
      ResponseCode1 = responseCode1;
    }

    public byte ProtocolVersion { get; }
    public byte RequestType0 { get; }
    public byte RequestType1 { get; }
    public byte RequestType2 { get; }
    public byte RequestType3 { get; }
    public ushort Counter { get; }
    public byte ResponseCode0 { get; }
    public byte ResponseCode1 { get; }
  }
}