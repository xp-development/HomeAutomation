using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Protocols.App.v0
{
  public class CommonErrorResponse : IResponse
  {
    public CommonErrorResponse(byte responseCode0, byte responseCode1)
    {
      ResponseCode0 = responseCode0;
      ResponseCode1 = responseCode1;
    }

    public ushort Counter { get; set; }
    public byte ResponseCode0 { get; set; }
    public byte ResponseCode1 { get; set; }
  }
}