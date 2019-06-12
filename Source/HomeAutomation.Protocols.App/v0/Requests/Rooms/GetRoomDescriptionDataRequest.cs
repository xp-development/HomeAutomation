namespace HomeAutomation.Protocols.App.v0.Requests.Rooms
{
  [RequestType(0x02, 0x02, 0x00, 0x00)]
  public class GetRoomDescriptionDataRequest : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
  }
}