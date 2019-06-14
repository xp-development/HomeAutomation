using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Server.Core.RequestHandlers
{
  [RequestType(1, 0, 0, 0)]
  public class ConnectRequestHandler : IRequestHandler
  {
    private readonly IConnectionHandler _connectionHandler;

    public ConnectRequestHandler(IConnectionHandler connectionHandler)
    {
      _connectionHandler = connectionHandler;
    }

    public IResponse Handle(IRequest request)
    {
      var bytes = _connectionHandler.NewConnection();

      return new ConnectDataResponse
      {
        ConnectionIdentifier0 = bytes[0],
        ConnectionIdentifier1 = bytes[1],
        ConnectionIdentifier2 = bytes[2],
        ConnectionIdentifier3 = bytes[3]
      };
    }
  }
}