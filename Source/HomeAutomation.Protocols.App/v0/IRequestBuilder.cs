using HomeAutomation.Protocols.App.v0.Requests;

namespace HomeAutomation.Protocols.App.v0
{
  public interface IRequestBuilder
  {
    byte[] Build(IRequest request);
  }
}