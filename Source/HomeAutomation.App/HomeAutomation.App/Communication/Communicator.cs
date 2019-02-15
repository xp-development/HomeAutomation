using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.App.Communication
{
  public class Communicator : ICommunicator
  {
    private readonly IUserSettings _userSettings;
    private readonly TcpClient _tcpClient;

    public Communicator(IUserSettings userSettings)
    {
      _userSettings = userSettings;
      _tcpClient = new TcpClient();
    }

    public async Task SendAsync(byte[] dataBytes)
    {
      if (!_tcpClient.Connected)
      {
        StartReceivingData();
        await _tcpClient.ConnectAsync(IPAddress.Parse(_userSettings.GetString("ServerIP")), _userSettings.GetInt32("ServerPort"));
      }

      await _tcpClient.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);
    }

    private void StartReceivingData()
    {
      Task.Run(async () =>
      {
        while (_tcpClient.Connected)
        {
          var stream = _tcpClient.GetStream();
          
//          await stream.ReadAsync();
        }
      });
    }

    public event Action<IResponse> ReceiveData;
  }
}