namespace HomeAutomation.Protocols.App.v0.Responses
{
  public abstract class GetAllObjectsDataResponseBase : ConnectionIdentificationResponseBase
  {
    public int[] Identifiers { get; set; }
  }
}