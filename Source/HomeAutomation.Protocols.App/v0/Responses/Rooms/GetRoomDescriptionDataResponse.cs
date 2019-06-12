namespace HomeAutomation.Protocols.App.v0.Responses.Rooms
{
  [RequestType(0x02, 0x02, 0x00, 0x00)]
  public class GetRoomDescriptionDataResponse : ConnectionIdentificationResponseBase
  {
    public int RoomIdentifier { get; set; }
    public string Description { get; set; }
  }
}