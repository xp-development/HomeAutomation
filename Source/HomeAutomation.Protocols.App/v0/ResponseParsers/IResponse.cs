namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IResponse
  {
    byte ProtocolVersion { get; }
    byte RequestType0 { get; }
    byte RequestType1 { get; }
    byte RequestType2 { get; }
    byte RequestType3 { get; }
    ushort Counter { get; }
    byte ResponseCode0 { get; }
    byte ResponseCode1 { get; }
  }
}