using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MetroLog;

namespace HomeAutomation.Server.Core
{
  public class TcpServer : ITcpServer
  {
    public event Action<ITcpServer, TcpClient, byte[]> DataReceived;
    private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<TcpServer>();

    public Task<object> StartAsync()
    {
      return Task.Run<object>(() =>
      {
        var server = new TcpListener(IPAddress.Any, 42123);
        server.Start();
        var client = server.AcceptTcpClient();
        var bytes = new byte[ushort.MaxValue];

        while (true)
        {
          var read = client.GetStream().Read(bytes, 0, bytes.Length);
          if (read <= 0)
            continue;

          var dataBytes = bytes.Take(read).ToArray();
          InvokeDataReceived(client, dataBytes);
        }

//        client.Close();
      });
    }

    public void SendData(TcpClient tcpClient, byte[] dataBytes)
    {
      Log.Debug($"Send: {BitConverter.ToString(dataBytes)}");
      var stream = tcpClient.GetStream();
      stream.Write(dataBytes, 0, dataBytes.Length);
    }

    private void InvokeDataReceived(TcpClient tcpClient, byte[] dataBytes)
    {
      Log.Debug($"Receive: {BitConverter.ToString(dataBytes)}");
      DataReceived?.Invoke(this, tcpClient, dataBytes);
    }
  }
}