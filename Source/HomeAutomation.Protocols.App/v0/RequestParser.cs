using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Requests;

namespace HomeAutomation.Protocols.App.v0
{
  public class RequestParser : IRequestParser
  {
    private readonly IDataConverterDispatcher _dataConverterDispatcher;
    private readonly IDictionary<RequestTypeAttribute, Type> _requestTypes;

    public RequestParser(IDataConverterDispatcher dataConverterDispatcher)
    {
      _dataConverterDispatcher = dataConverterDispatcher;
      _requestTypes = GetRequestTypes();
    }

    public IRequest Parse(byte[] dataBytes)
    {
      if (dataBytes == null || dataBytes.Length < 9)
        throw new CorruptDataException();

      var protocolVersion = dataBytes[0];
      if (protocolVersion != 0x00)
        throw new NotSupportedProtocolException(protocolVersion);

      var requestType0 = dataBytes[1];
      var requestType1 = dataBytes[2];
      var requestType2 = dataBytes[3];
      var requestType3 = dataBytes[4];

      if (!_requestTypes.TryGetValue(new RequestTypeAttribute(requestType0, requestType1, requestType2, requestType3), out var requestType))
        throw new UnknownCommandException();

      var dataLength = BitConverter.ToUInt16(dataBytes, 5);

      var crc0 = dataBytes[7 + dataLength];
      var crc1 = dataBytes[8 + dataLength];

      var computeChecksum = Crc16.ComputeChecksum(dataBytes.Take(dataBytes.Length - 2));
      if (crc0 != computeChecksum[0] || crc1 != computeChecksum[1])
        throw new WrongCrcException();

      var request = Activator.CreateInstance(requestType);
      var dataIndex = 7;
      foreach (var propertyInfo in requestType.GetAllProperties())
      {
        object dataValue;
        (dataValue, dataIndex) = _dataConverterDispatcher.ConvertBack(propertyInfo.PropertyType, dataBytes, dataIndex);
        propertyInfo.SetValue(request, dataValue);
      }

      return (IRequest) request;
    }

    private static IDictionary<RequestTypeAttribute, Type> GetRequestTypes()
    {
      return typeof(RequestParser).Assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(RequestTypeAttribute), true).Length > 0).Where(type => type.GetInterface(nameof(IRequest)) != null).ToDictionary(x => (RequestTypeAttribute)x.GetCustomAttributes(typeof(RequestTypeAttribute), true)[0], x => x);
    }
  }
}