namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IV0ResponseParser
  {
    IResponse Parse(byte[] dataBytes);
  }
}