namespace HomeAutomation.Protocols.App.v0.Responses.Rooms
{
  [RequestType(0x02, 0x00, 0x00, 0x00)]
  public class CreateRoomDataResponse : ConnectionIdentificationResponseBase
  {
    public byte ClientRoomIdentifier { get; set; }
    public int RoomIdentifier { get; set; }
  }
}