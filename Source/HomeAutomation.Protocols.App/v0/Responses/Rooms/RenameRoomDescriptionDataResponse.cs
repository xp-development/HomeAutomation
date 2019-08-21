namespace HomeAutomation.Protocols.App.v0.Responses.Rooms
{
  [RequestType(0x02, 0x03, 0x00, 0x00)]
  public class RenameRoomDescriptionDataResponse : ConnectionIdentificationResponseBase
  {
    public int RoomIdentifier { get; set; }
  }
}