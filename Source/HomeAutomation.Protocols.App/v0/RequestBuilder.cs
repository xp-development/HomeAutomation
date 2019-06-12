using System;
using System.Collections.Generic;
using System.Reflection;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Requests;

namespace HomeAutomation.Protocols.App.v0
{
  public class RequestBuilder : IRequestBuilder
  {
    private readonly IDataConverterDispatcher _dataConverterDispatcher;

    public RequestBuilder(IDataConverterDispatcher dataConverterDispatcher)
    {
      _dataConverterDispatcher = dataConverterDispatcher;
    }

    public byte[] Build(IRequest request)
    {
      var requestType = request.GetType();
      var requestTypeAttribute = (RequestTypeAttribute)requestType.GetCustomAttribute(typeof(RequestTypeAttribute));

      var requestBytes = new List<byte>();

      const byte protocolVersion = 0x00;
      requestBytes.Add(protocolVersion);
      requestBytes.Add(requestTypeAttribute.RequestType0);
      requestBytes.Add(requestTypeAttribute.RequestType1);
      requestBytes.Add(requestTypeAttribute.RequestType2);
      requestBytes.Add(requestTypeAttribute.RequestType3);

      var dataBytes = new List<byte>();
      foreach (var propertyInfo in requestType.GetAllProperties())
      {
        var value = propertyInfo.GetValue(request);
        dataBytes.AddRange(_dataConverterDispatcher.Convert(value));
      }

      requestBytes.AddRange(BitConverter.GetBytes((ushort)dataBytes.Count));
      requestBytes.AddRange(dataBytes);
      var checksum = Crc16.ComputeChecksum(requestBytes);
      requestBytes.Add(checksum[0]);
      requestBytes.Add(checksum[1]);
      return requestBytes.ToArray();
    }
  }
}