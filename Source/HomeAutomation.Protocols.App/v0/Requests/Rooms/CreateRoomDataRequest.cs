namespace HomeAutomation.Protocols.App.v0.Requests.Rooms
{
  [RequestType(0x02, 0x00, 0x00, 0x00)]
  public class CreateRoomDataRequest : ConnectionRequiredRequestBase
  {
    public byte ClientRoomIdentifier { get; set; }
    public string Description { get; set; }
  }
}