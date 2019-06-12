namespace HomeAutomation.Protocols.App.v0.Requests
{
  public class ConnectionRequiredRequestBase : IConnectionRequiredRequest
  {
    public byte ConnectionIdentifier0 { get; set; }
    public byte ConnectionIdentifier1 { get; set; }
    public byte ConnectionIdentifier2 { get; set; }
    public byte ConnectionIdentifier3 { get; set; }
  }
}