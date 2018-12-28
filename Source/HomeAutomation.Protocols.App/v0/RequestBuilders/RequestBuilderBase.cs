using System;
using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public abstract class RequestBuilderBase
  {
    private readonly ICounter _counter;
    private byte[] _dataBytes;

    protected RequestBuilderBase(ICounter counter)
    {
      _counter = counter;
    }

    protected abstract byte RequestType0 { get; }
    protected abstract byte RequestType1 { get; }
    protected abstract byte RequestType2 { get; }
    protected abstract byte RequestType3 { get; }

    public byte[] Data { get; private set; }

    public byte[] Build(params byte[] dataBytes)
    {
      Data = dataBytes ?? new byte[0];
      return OnBuild().ToArray();
    }

    protected virtual List<byte> OnBuild()
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
      build.AddRange(Data);

      var checksum = Crc16.ComputeChecksum(build);
      build.Add(checksum[0]);
      build.Add(checksum[1]);
      return build;
    }
  }
}