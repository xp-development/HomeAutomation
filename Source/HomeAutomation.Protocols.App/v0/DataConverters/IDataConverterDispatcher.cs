using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public interface IDataConverterDispatcher
  {
    IEnumerable<byte> Convert(object value);
    (object dataValue, int dataIndex) ConvertBack(Type propertyType, byte[] dataBytes, int dataIndex);
  }
}