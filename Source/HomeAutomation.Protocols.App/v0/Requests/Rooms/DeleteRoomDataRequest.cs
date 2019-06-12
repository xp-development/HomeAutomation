namespace HomeAutomation.Protocols.App.v0.Requests.Rooms
{
  [RequestType(0x02, 0x04, 0x00, 0x00)]
  public class DeleteRoomDataRequest : ConnectionRequiredRequestBase
  {
    public int RoomIdentifier { get; set; }
  }
}