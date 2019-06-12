using System;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Server.Core.RequestHandlers
{
  [RequestType(1, 0, 0, 0)]
  public class ConnectRequestHandler : IRequestHandler
  {
    private static uint _currentIdentifier;

    public IResponse Handle(IRequest request)
    {
      var identifier = ++_currentIdentifier;

      var bytes = BitConverter.GetBytes(identifier);

      return new ConnectDataResponse
      {
        ConnectionIdentifier0 = bytes[0],
        ConnectionIdentifier1 = bytes[1],
        ConnectionIdentifier2 = bytes[2],
        ConnectionIdentifier3 = bytes[3]
      };
    }

    public static void Reset()
    {
      _currentIdentifier = 0;
    }
  }
}