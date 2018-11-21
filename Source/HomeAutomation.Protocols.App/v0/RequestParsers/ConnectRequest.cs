using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public class ConnectRequest : IRequest
  {
    public ConnectRequest(byte protocolVersion, ushort counter)
    {
      ProtocolVersion = protocolVersion;
      Counter = counter;
    }

    public byte ProtocolVersion { get; }
    public byte RequestType0 => 0x01;
    public byte RequestType1 => 0x00;
    public byte RequestType2 => 0x00;
    public byte RequestType3 => 0x00;
    public ushort Counter { get; }
  }
}