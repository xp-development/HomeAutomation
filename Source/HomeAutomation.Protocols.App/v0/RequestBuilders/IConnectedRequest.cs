namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public interface IConnectedRequest : IRequest
  {
    uint ConnectionIdentifier { get; }
  }
}