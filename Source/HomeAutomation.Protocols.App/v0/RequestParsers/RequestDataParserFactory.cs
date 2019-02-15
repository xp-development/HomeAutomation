using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public class RequestDataParserFactory : IRequestDataParserFactory
  {
    private readonly List<IRequestDataParser> _requestDataParsers;

    public RequestDataParserFactory(List<IRequestDataParser> requestDataParsers)
    {
      _requestDataParsers = requestDataParsers;
    }

    public IRequestDataParser TryCreate(byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      return _requestDataParsers.FirstOrDefault(x => x.RequestType0 == requestType0
                                                     && x.RequestType1 == requestType1
                                                     && x.RequestType2 == requestType2
                                                     && x.RequestType3 == requestType3);
    }
  }
}