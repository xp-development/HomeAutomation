namespace HomeAutomation.Protocols.App.v0.Responses
{
  public abstract class GetObjectDescriptionDataResponseBase : ConnectionIdentificationResponseBase
  {
    public int Identifier { get; set; }
    public string Description { get; set; }
  }
}