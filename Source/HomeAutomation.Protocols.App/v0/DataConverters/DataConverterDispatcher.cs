using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.DataConverters
{
  public class DataConverterDispatcher : IDataConverterDispatcher
  {
    private readonly IEnumerable<IDataConverter> _dataConverters;

    public DataConverterDispatcher(IEnumerable<IDataConverter> dataConverters)
    {
      _dataConverters = dataConverters;
    }

    public IEnumerable<byte> Convert(object value)
    {
      var valueType = value.GetType();
      return _dataConverters.Single(x => x.DataType == valueType).Convert(value);
    }

    public (object dataValue, int dataIndex) ConvertBack(Type propertyType, byte[] dataBytes, int dataIndex)
    {
      return _dataConverters.Single(x => x.DataType == propertyType).ConvertBack(dataBytes, dataIndex);
    }
  }
}