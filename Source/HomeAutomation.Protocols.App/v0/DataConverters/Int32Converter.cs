using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class Int32Converter : IDataConverter
  {
    public Type DataType { get; } = typeof(int);

    public IEnumerable<byte> Convert(object data)
    {
      return BitConverter.GetBytes((int)data);
    }

    public (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex)
    {
      return (BitConverter.ToInt32(dataBytes, dataIndex), dataIndex + 4);
    }
  }
}