namespace HomeAutomation.Protocols.App.v0.Responses.Devices
{
  [RequestType(0x03, 0x03, 0x00, 0x00)]
  public class RenameDeviceDescriptionDataResponse : ConnectionIdentificationResponseBase
  {
    public int Identifier { get; set; }
  }
}