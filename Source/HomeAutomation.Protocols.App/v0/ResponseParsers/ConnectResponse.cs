namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class ConnectResponse : IResponse
  {
    public byte ProtocolVersion { get; }
    public byte RequestType0 => 0x01;
    public byte RequestType1 => 0x00;
    public byte RequestType2 => 0x00;
    public byte RequestType3 => 0x00;
    public ushort Counter { get; }
    public byte ResponseCode0 { get; }
    public byte ResponseCode1 { get; }
    public uint Identifier { get; }

    public ConnectResponse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, uint identifier)
    {
      ProtocolVersion = protocolVersion;
      Counter = counter;
      ResponseCode0 = responseCode0;
      ResponseCode1 = responseCode1;
      Identifier = identifier;
    }
  }
}