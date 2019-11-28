namespace HomeAutomation.Protocols.App.v0.Responses
{
  public abstract class CreateObjectDataResponseBase : ConnectionIdentificationResponseBase
  {
    public byte ClientObjectIdentifier { get; set; }
    public int Identifier { get; set; }
  }
}