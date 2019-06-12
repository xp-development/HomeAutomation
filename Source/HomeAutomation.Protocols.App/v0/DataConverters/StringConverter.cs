using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class StringConverter : IDataConverter
  {
    public Type DataType { get; } = typeof(string);

    public IEnumerable<byte> Convert(object data)
    {
      var bytes = Encoding.Unicode.GetBytes((string) data);
      var dataLengthBytes = BitConverter.GetBytes((ushort)bytes.Length);
      yield return dataLengthBytes[0];
      yield return dataLengthBytes[1];
      foreach (var @byte in bytes)
        yield return @byte;
    }

    public (object dataValue, int nextDataIndex) ConvertBack(byte[] dataBytes, int dataIndex)
    {
      var dataLength = BitConverter.ToUInt16(dataBytes, dataIndex);
      return (Encoding.Unicode.GetString(dataBytes, dataIndex + 2, dataLength), dataIndex + dataLength + 2);
    }
  }
}