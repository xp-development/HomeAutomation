namespace HomeAutomation.Protocols.App.v0
{
  public class NotSupportedProtocolException : HomeAutomationException
  {
    public byte ProtocolVersion { get; }

    public NotSupportedProtocolException(byte protocolVersion)
    {
      ProtocolVersion = protocolVersion;
    }
  }
}