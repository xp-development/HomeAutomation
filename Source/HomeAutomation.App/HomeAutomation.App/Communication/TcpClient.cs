using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HomeAutomation.App.Communication
{
  public class TcpClient : ITcpClient
  {
    private readonly System.Net.Sockets.TcpClient _tcpClient;

    public TcpClient()
    {
      _tcpClient = new System.Net.Sockets.TcpClient();
    }

    public bool Connected => _tcpClient.Connected;

    public Task ConnectAsync(IPAddress address, int port)
    {
      return _tcpClient.ConnectAsync(address, port);
    }

    public Task WriteAsync(byte[] dataBytes)
    {
      return _tcpClient.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);
    }

    public async Task<byte[]> ReadAsync()
    {
      var bytes = new byte[ushort.MaxValue];
      var dataLength = await _tcpClient.GetStream().ReadAsync(bytes, 0 , ushort.MaxValue);
      return bytes.Take(dataLength).ToArray();
    }
  }
}