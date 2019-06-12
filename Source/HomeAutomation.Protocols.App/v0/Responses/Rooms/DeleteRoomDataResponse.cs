namespace HomeAutomation.Protocols.App.v0.Responses.Rooms
{
  [RequestType(0x02, 0x04, 0x00, 0x00)]
  public class DeleteRoomDataResponse : ConnectionIdentificationResponseBase
  {
    public int RoomIdentifier { get; set; }
  }
}