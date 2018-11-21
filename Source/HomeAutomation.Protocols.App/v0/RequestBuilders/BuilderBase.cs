using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public abstract class BuilderBase
  {
    private readonly ICounter _counter;

    protected BuilderBase(ICounter counter)
    {
      _counter = counter;
    }

    protected abstract byte RequestType0 { get; }
    protected abstract byte RequestType1 { get; }
    protected abstract byte RequestType2 { get; }
    protected abstract byte RequestType3 { get; }

    protected abstract byte[] Data { get; }

    public IEnumerable<byte> Build()
    {
      var build = new List<byte>
      {
        0x00, //protocol version
        RequestType0,
        RequestType1,
        RequestType2,
        RequestType3
      };

      var count = _counter.GetNext();
      build.Add(count[0]);
      build.Add(count[1]);

      var dataLength = BitConverter.GetBytes(Data.Length);
      build.Add(dataLength[0]);
      build.Add(dataLength[1]);
      build.Add(dataLength[2]);
      build.Add(dataLength[3]);
      build.AddRange(Data);

      var checksum = Crc16.ComputeChecksum(build);
      build.Add(checksum[0]);
      build.Add(checksum[1]);
      return build;
    }
  }
}