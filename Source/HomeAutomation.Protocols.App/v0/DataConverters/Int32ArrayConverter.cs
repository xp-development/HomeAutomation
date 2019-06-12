using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class Int32ArrayConverter : IDataConverter
  {
    public Type DataType { get; } = typeof(int[]);

    public IEnumerable<byte> Convert(object data)
    {
      var ints = (int[])data;
      var bytes = new List<byte>();
      bytes.AddRange(BitConverter.GetBytes((ushort)ints.Length));
      foreach (var i in ints)
        bytes.AddRange(BitConverter.GetBytes(i));

      return bytes;
    }

    public (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex)
    {
      var intCount = BitConverter.ToUInt16(dataBytes, dataIndex);
      var ints = new int[intCount];
      for (int i = 0; i < intCount; ++i)
      {
        ints[i] = BitConverter.ToInt32(dataBytes, i * 4 + dataIndex + 2);
      }

      return (ints.ToArray(), dataIndex + intCount * 4 + 2);
    }
  }
}