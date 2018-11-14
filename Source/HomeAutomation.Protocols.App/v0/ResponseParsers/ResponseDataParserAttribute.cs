using System;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class ResponseDataParserAttribute : Attribute
  {
    public byte RequestType0 { get; }
    public byte RequestType1 { get; }
    public byte RequestType2 { get; }
    public byte RequestType3 { get; }

    public ResponseDataParserAttribute(byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      RequestType0 = requestType0;
      RequestType1 = requestType1;
      RequestType2 = requestType2;
      RequestType3 = requestType3;
    }
  }
}