using System.Net;
using System.Threading.Tasks;

namespace HomeAutomation.App.Communication
{
  public interface ITcpClient
  {
    bool Connected { get; }
    Task ConnectAsync(IPAddress address, int port);
    Task WriteAsync(byte[] dataBytes);
    Task<byte[]> ReadAsync();
  }
}