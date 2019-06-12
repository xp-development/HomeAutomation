using HomeAutomation.Protocols.App.v0.Requests;

namespace HomeAutomation.Protocols.App.v0
{
  public interface IRequestParser
  {
    IRequest Parse(byte[] dataBytes);
  }
}