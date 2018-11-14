using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class ResponseDataParserFactory : IResponseDataParserFactory
  {
    private readonly Dictionary<Tuple<byte, byte, byte, byte>, Type> _dataParsers = new Dictionary<Tuple<byte, byte, byte, byte>, Type>();

    public IResponseDataParser TryCreate(byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      var key = Tuple.Create(requestType0, requestType1, requestType2, requestType3);
      var parserType = _dataParsers.FirstOrDefault(x => Equals(x.Key, key)).Value;
      if (parserType == null)
        return null;

      return (IResponseDataParser) Activator.CreateInstance(parserType);
    }

    public void Register<TResponseDataParser>()
      where TResponseDataParser : IResponseDataParser
    {
      var attribute = (ResponseDataParserAttribute)typeof(TResponseDataParser).GetCustomAttribute(typeof(ResponseDataParserAttribute));
      _dataParsers.Add(Tuple.Create(attribute.RequestType0, attribute.RequestType1, attribute.RequestType2, attribute.RequestType3), typeof(TResponseDataParser));
    }
  }
}