using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HomeAutomation.Server.Core
{
  public class TcpServer : ITcpServer
  {
    public event Action<ITcpServer, TcpClient, byte[]> DataReceived;

    public Task<object> StartAsync()
    {
      return Task.Run<object>(() =>
      {
        var server = new TcpListener(IPAddress.Any, 42123);
        server.Start();
        while (true)
        {
          var client = server.AcceptTcpClient();
          var bytes = new byte[ushort.MaxValue];
          while (client.GetStream().Read(bytes, 0, bytes.Length) != 0)
          {
            InvokeDataReceived(client, bytes);
          }
          client.Close();
        }
      });
    }

    public void SendData(TcpClient tcpClient, byte[] dataBytes)
    {
      var stream = tcpClient.GetStream();
      stream.Write(dataBytes, 0, dataBytes.Length);
    }

    private void InvokeDataReceived(TcpClient tcpClient, byte[] dataBytes)
    {
      DataReceived?.Invoke(this, tcpClient, dataBytes);
    }
  }
}
