using System;

namespace HomeAutomation.Protocols.App.v0.Responses
{
  public class CommonErrorResponse : IResponse, IHaveResponseTypeInformation
  {
    public CommonErrorResponse(CommonResponseCode commonResponseCode, ushort counter, byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      var commonResponseCodeBytes = BitConverter.GetBytes((ushort) commonResponseCode);
      Initialize(counter, requestType0, requestType1, requestType2, requestType3, commonResponseCodeBytes[1], commonResponseCodeBytes[0]);
    }

    public CommonErrorResponse(byte responseCode0, byte responseCode1, ushort counter, byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      Initialize(counter, requestType0, requestType1, requestType2, requestType3, responseCode0, responseCode1);
    }

    private void Initialize(ushort counter, byte requestType0, byte requestType1, byte requestType2, byte requestType3,
      byte responseCode0, byte responseCode1)
    {
      Counter = counter;
      RequestType0 = requestType0;
      RequestType1 = requestType1;
      RequestType2 = requestType2;
      RequestType3 = requestType3;
      ResponseCode0 = responseCode0;
      ResponseCode1 = responseCode1;
    }

    public ushort Counter { get; set; }
    public byte ResponseCode0 { get; set; }
    public byte ResponseCode1 { get; set; }
    public byte RequestType0 { get; private set; }
    public byte RequestType1 { get; private set; }
    public byte RequestType2 { get; private set; }
    public byte RequestType3 { get; private set; }
  }
}