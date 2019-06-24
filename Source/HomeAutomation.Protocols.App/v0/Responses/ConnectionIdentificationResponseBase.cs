namespace HomeAutomation.Protocols.App.v0.Responses
{
  public class ConnectionIdentificationResponseBase : ResponseBase
  {
    public byte ConnectionIdentifier0 { get; set; }
    public byte ConnectionIdentifier1 { get; set; }
    public byte ConnectionIdentifier2 { get; set; }
    public byte ConnectionIdentifier3 { get; set; }
  }
}