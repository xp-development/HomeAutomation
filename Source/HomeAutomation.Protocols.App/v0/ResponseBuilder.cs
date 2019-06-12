using System;
using System.Collections.Generic;
using System.Reflection;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Protocols.App.v0
{
  public class ResponseBuilder : IResponseBuilder
  {
    private readonly IDataConverterDispatcher _dataConverterDispatcher;

    public ResponseBuilder(IDataConverterDispatcher dataConverterDispatcher)
    {
      _dataConverterDispatcher = dataConverterDispatcher;
    }

    public byte[] Build(IResponse response)
    {
      var responseType = response.GetType();
      var responseTypeAttribute = (RequestTypeAttribute)responseType.GetCustomAttribute(typeof(RequestTypeAttribute));
      
      var responseBytes = new List<byte>();

      const byte protocolVersion = 0x00;
      responseBytes.Add(protocolVersion);
      responseBytes.Add(responseTypeAttribute.RequestType0);
      responseBytes.Add(responseTypeAttribute.RequestType1);
      responseBytes.Add(responseTypeAttribute.RequestType2);
      responseBytes.Add(responseTypeAttribute.RequestType3);

      var dataBytes = new List<byte>();
      foreach (var propertyInfo in responseType.GetAllProperties())
      {
        var value = propertyInfo.GetValue(response);
        dataBytes.AddRange(_dataConverterDispatcher.Convert(value));
      }

      responseBytes.AddRange(BitConverter.GetBytes((ushort)dataBytes.Count));
      responseBytes.AddRange(dataBytes);
      var checksum = Crc16.ComputeChecksum(responseBytes);
      responseBytes.Add(checksum[0]);
      responseBytes.Add(checksum[1]);
      return responseBytes.ToArray();
    }
  }
}