namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IResponseParser
  {
    IResponse Parse(byte[] dataBytes);
  }
}