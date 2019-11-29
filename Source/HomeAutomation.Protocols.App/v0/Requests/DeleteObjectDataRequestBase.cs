namespace HomeAutomation.Protocols.App.v0.Requests
{
  public class DeleteObjectDataRequestBase : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
  }
}