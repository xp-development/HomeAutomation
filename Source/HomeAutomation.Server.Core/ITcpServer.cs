using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HomeAutomation.Server.Core
{
  public interface ITcpServer
  {
    event Action<ITcpServer, TcpClient, byte[]> DataReceived;
    Task<object> StartAsync();
    void SendData(TcpClient tcpClient, byte[] dataBytes);
  }
}