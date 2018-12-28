using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.App.Communication
{
  public class Communicator : ICommunicator
  {
    private TcpClient _tcpClient;

    public Communicator()
    {
      var tcpClient = new TcpClient();
      
      var stream = tcpClient.GetStream();
      
//      stream.Write(data, 0, data.Length);
//      Int32 bytes = stream.Read(data, 0, data.Length);
//      responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    }

    public Task ConnectAsync()
    {
      _tcpClient = new TcpClient();
      return Task.CompletedTask;
    }

    public Task DisconnectAsync()
    {
      _tcpClient = null;
      return Task.CompletedTask;
    }

    public Task SendAsync(byte[] dataBytes)
    {
      throw new NotImplementedException();
    }

    public event Action<IResponse> ReceiveData;
  }
}