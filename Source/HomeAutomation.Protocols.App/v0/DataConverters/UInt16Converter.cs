using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class UInt16Converter : IDataConverter
  {
    public Type DataType { get; } = typeof(ushort);

    public IEnumerable<byte> Convert(object data)
    {
      return BitConverter.GetBytes((ushort)data);
    }

    public (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex)
    {
      return (BitConverter.ToUInt16(dataBytes, dataIndex), dataIndex + 2);
    }
  }
}