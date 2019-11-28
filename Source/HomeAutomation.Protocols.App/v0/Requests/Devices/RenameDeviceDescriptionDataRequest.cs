namespace HomeAutomation.Protocols.App.v0.Requests.Devices
{
  [RequestType(0x03, 0x03, 0x00, 0x00)]
  public class RenameDeviceDescriptionDataRequest : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
    public string Description { get; set; }
  }
}