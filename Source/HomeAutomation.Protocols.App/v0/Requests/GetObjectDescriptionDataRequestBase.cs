namespace HomeAutomation.Protocols.App.v0.Requests
{
  public abstract class GetObjectDescriptionDataRequestBase : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
  }
}