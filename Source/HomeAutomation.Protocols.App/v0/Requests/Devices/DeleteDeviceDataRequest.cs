namespace HomeAutomation.Protocols.App.v0.Requests.Devices
{
  [RequestType(0x03, 0x04, 0x00, 0x00)]
  public class DeleteDeviceDataRequest : ConnectionRequiredRequestBase
  {
    public int Identifier { get; set; }
  }
}