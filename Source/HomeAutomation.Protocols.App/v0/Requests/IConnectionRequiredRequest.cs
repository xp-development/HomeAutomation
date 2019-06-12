namespace HomeAutomation.Protocols.App.v0.Requests
{
  public interface IConnectionRequiredRequest : IRequest
  {
    byte ConnectionIdentifier0 { get; set; }
    byte ConnectionIdentifier1 { get; set; }
    byte ConnectionIdentifier2 { get; set; }
    byte ConnectionIdentifier3 { get; set; }
  }
}