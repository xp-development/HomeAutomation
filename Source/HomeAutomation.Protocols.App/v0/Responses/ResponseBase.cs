namespace HomeAutomation.Protocols.App.v0.Responses
{
  public class ResponseBase : IResponse
  {
    public ushort Counter { get; set; }
    public byte ResponseCode0 { get; set; }
    public byte ResponseCode1 { get; set; }
  }
}