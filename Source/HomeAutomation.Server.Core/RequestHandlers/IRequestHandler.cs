using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Server.Core.RequestHandlers
{
  public interface IRequestHandler
  {
    IResponse Handle(IRequest request);
  }
}