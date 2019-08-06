using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Protocols.App.v0
{
  public class ResponseParser : IResponseParser
  {
    private readonly IDataConverterDispatcher _dataConverterDispatcher;
    private readonly IDictionary<RequestTypeAttribute, Type> _responseTypes;

    public ResponseParser(IDataConverterDispatcher dataConverterDispatcher)
    {
      _dataConverterDispatcher = dataConverterDispatcher;
      _responseTypes = GetResponseTypes();
    }

    public IResponse Parse(byte[] dataBytes)
    {
      if(dataBytes == null || dataBytes.Length < 13)
        return new CommonErrorResponse(CommonResponseCode.CorruptData, 0, 0, 0, 0, 0);

      var requestType0 = dataBytes[1];
      var requestType1 = dataBytes[2];
      var requestType2 = dataBytes[3];
      var requestType3 = dataBytes[4];
      var counter = BitConverter.ToUInt16(dataBytes, 7);

      var protocolVersion = dataBytes[0];
      if (protocolVersion != 0x00)
        return new CommonErrorResponse(CommonResponseCode.NotSupportedProtocolVersion, counter, requestType0, requestType1, requestType2, requestType3);

      if (!_responseTypes.TryGetValue(new RequestTypeAttribute(requestType0, requestType1, requestType2, requestType3), out var responseType))
        return new CommonErrorResponse(CommonResponseCode.UnknownCommand, counter, requestType0, requestType1, requestType2, requestType3);

      var dataLength = BitConverter.ToUInt16(dataBytes, 5);

      var crc0 = dataBytes[7 + dataLength];
      var crc1 = dataBytes[8 + dataLength];

      var computeChecksum = Crc16.ComputeChecksum(dataBytes.Take(dataBytes.Length - 2));
      if (crc0 != computeChecksum[0] || crc1 != computeChecksum[1])
        return new CommonErrorResponse(CommonResponseCode.WrongCrc, counter, requestType0, requestType1, requestType2, requestType3);

      var responseCode0 = dataBytes[9];
      var responseCode1 = dataBytes[10];

      if(responseCode0 != 0x00 || responseCode1 != 0x00)
        return new CommonErrorResponse(responseCode0, responseCode1, counter, requestType0, requestType1, requestType2, requestType3);

      var response = Activator.CreateInstance(responseType);
      var dataIndex = 7;
      foreach (var propertyInfo in responseType.GetAllProperties())
      {
        object dataValue;
        (dataValue, dataIndex) = _dataConverterDispatcher.ConvertBack(propertyInfo.PropertyType, dataBytes, dataIndex);
        propertyInfo.SetValue(response, dataValue);
      }

      return (IResponse) response;
    }

    private static IDictionary<RequestTypeAttribute, Type> GetResponseTypes()
    {
      return typeof(ResponseParser).Assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(RequestTypeAttribute), true).Length > 0).Where(type => type.GetInterface(nameof(IResponse)) != null).ToDictionary(x => (RequestTypeAttribute)x.GetCustomAttributes(typeof(RequestTypeAttribute), true)[0], x => x);
    }
  }
}