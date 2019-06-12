namespace HomeAutomation.Protocols.App.v0.Requests.Rooms
{
  [RequestType(0x02, 0x03, 0x00, 0x00)]
  public class RenameRoomDescriptionDataRequest : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
    public string Description { get; set; }
  }
}