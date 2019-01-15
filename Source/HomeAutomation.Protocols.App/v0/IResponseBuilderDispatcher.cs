using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0
{
  public interface IResponseBuilderDispatcher
  {
    byte[] Build(IRequest request);
  }
}