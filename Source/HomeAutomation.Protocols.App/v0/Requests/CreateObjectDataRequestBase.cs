namespace HomeAutomation.Protocols.App.v0.Requests
{
  public abstract class CreateObjectDataRequestBase : ConnectionRequiredRequestBase
  {
    public byte ClientObjectIdentifier { get; set; }
    public string Description { get; set; }
  }
}