using System;
using System.Collections.Generic;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.ResponseBuilders
{
  public abstract class ResponseBuilderBase
  {
    public byte[] Build(IRequest request)
    {
      var bytes = new List<byte>
      {
        request.ProtocolVersion,
        request.RequestType0,
        request.RequestType1,
        request.RequestType2,
        request.RequestType3
      };

      bytes.AddRange(BitConverter.GetBytes(request.Counter));
      bytes.AddRange(OnBuild(request));
      bytes.AddRange(Crc16.ComputeChecksum(bytes));

      return bytes.ToArray();
    }

    protected abstract IEnumerable<byte> OnBuild(IRequest request);
  }
}