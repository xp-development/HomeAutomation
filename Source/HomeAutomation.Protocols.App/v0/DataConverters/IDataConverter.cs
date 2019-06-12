using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public interface IDataConverter
  {
    Type DataType { get; }
    IEnumerable<byte> Convert(object data);
    (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex);
  }
}