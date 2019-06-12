using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.Protocols.App.v0
{
  public interface IResponseParser
  {
    IResponse Parse(byte[] dataBytes);
  }
}