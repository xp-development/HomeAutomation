using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParsers
{
  public interface IRequestParser
  {
    IRequest Parse(byte[] dataBytes);
  }
}