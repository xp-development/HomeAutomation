using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public abstract class V0BuilderBase : IV0Request
  {
    private readonly ICounter _counter;

    protected V0BuilderBase(ICounter counter)
    {
      _counter = counter;
    }

    public abstract byte RequestType0 { get; }
    public abstract byte RequestType1 { get; }
    public abstract byte RequestType2 { get; }
    public abstract byte RequestType3 { get; }

    public abstract byte[] Data { get; }

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