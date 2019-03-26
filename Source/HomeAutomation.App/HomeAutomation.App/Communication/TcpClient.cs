using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MetroLog;

namespace HomeAutomation.App.Communication
{
  public class TcpClient : ITcpClient
  {
    private readonly System.Net.Sockets.TcpClient _tcpClient;
    private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<TcpClient>();

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
      Log.Debug($"Write: {BitConverter.ToString(dataBytes)}");
      return _tcpClient.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);
    }

    public async Task<byte[]> ReadAsync()
    {
      var bytes = new byte[ushort.MaxValue];
      var dataLength = await _tcpClient.GetStream().ReadAsync(bytes, 0 , ushort.MaxValue);
      var dataBytes = bytes.Take(dataLength).ToArray();
      Log.Debug($"Read: {BitConverter.ToString(dataBytes)}");
      return dataBytes;
    }
  }
}