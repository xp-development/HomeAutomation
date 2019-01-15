using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;

namespace HomeAutomation.Protocols.App.v0
{
  public class ResponseBuilderDispatcher : IResponseBuilderDispatcher
  {
    private readonly IEnumerable<IResponseBuilder> _responseBuilders;

    public ResponseBuilderDispatcher(IEnumerable<IResponseBuilder> responseBuilders)
    {
      _responseBuilders = responseBuilders;
    }

    public byte[] Build(IRequest request)
    {
      var requestType = request.GetType();
      return _responseBuilders.FirstOrDefault(x => x.RequestType == requestType)?.Build(request) ?? throw new ArgumentException($"No response builder registered for '{requestType}'");
    }
  }
}