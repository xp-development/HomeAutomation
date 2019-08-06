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

      var responseBytes = new List<byte>();

      const byte protocolVersion = 0x00;
      responseBytes.Add(protocolVersion);

      responseBytes.AddRange(GetResponseTypeBytes(responseType, response));

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

    private static IEnumerable<byte> GetResponseTypeBytes(MemberInfo responseType, IResponse response)
    {
      if (response is IHaveResponseTypeInformation responseTypeInformation)
      {
        yield return responseTypeInformation.RequestType0;
        yield return responseTypeInformation.RequestType1;
        yield return responseTypeInformation.RequestType2;
        yield return responseTypeInformation.RequestType3;
      }
      else
      {
        var responseTypeAttribute = (RequestTypeAttribute) responseType.GetCustomAttribute(typeof(RequestTypeAttribute));
        yield return responseTypeAttribute.RequestType0;
        yield return responseTypeAttribute.RequestType1;
        yield return responseTypeAttribute.RequestType2;
        yield return responseTypeAttribute.RequestType3;
      }
    }
  }
}