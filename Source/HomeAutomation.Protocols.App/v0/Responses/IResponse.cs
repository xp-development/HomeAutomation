namespace HomeAutomation.Protocols.App.v0.Responses
{
  public interface IResponse
  {
    ushort Counter { get; set; }
    byte ResponseCode0 { get; set; }
    byte ResponseCode1 { get; set; }
  }
}