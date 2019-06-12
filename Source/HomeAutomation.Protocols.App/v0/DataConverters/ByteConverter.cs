using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class ByteConverter : IDataConverter
  {
    public Type DataType { get; } = typeof(byte);

    public IEnumerable<byte> Convert(object data)
    {
      return new [] { (byte)data };
    }

    public (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex)
    {
      return (dataBytes[dataIndex], dataIndex + 1);
    }
  }
}