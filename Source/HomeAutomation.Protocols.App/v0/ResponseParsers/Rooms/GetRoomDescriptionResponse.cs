namespace HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms
{
  public class GetRoomDescriptionResponse : IResponse
  {
    public byte ProtocolVersion { get; }
    public byte RequestType0 => 0x02;
    public byte RequestType1 => 0x02;
    public byte RequestType2 => 0x00;
    public byte RequestType3 => 0x00;
    public ushort Counter { get; }
    public byte ResponseCode0 { get; }
    public byte ResponseCode1 { get; }
    public string Description { get; }

    public GetRoomDescriptionResponse(byte protocolVersion, ushort counter, byte responseCode0, byte responseCode1, string description)
    {
      ProtocolVersion = protocolVersion;
      Counter = counter;
      ResponseCode0 = responseCode0;
      ResponseCode1 = responseCode1;
      Description = description;
    }
  }
}