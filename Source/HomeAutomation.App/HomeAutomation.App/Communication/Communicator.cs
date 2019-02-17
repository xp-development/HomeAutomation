using System;
using System.Net;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.App.Communication
{
  public class Communicator : ICommunicator
  {
    private readonly ITcpClient _tcpClient;
    private readonly IUserSettings _userSettings;

    public Communicator(IUserSettings userSettings, ITcpClient tcpClient)
    {
      _tcpClient = tcpClient;
      _userSettings = userSettings;
    }

    public async Task SendAsync(byte[] dataBytes)
    {
      if (!_tcpClient.Connected)
      {
        StartReceivingData();
        await _tcpClient.ConnectAsync(IPAddress.Parse(_userSettings.GetString("ServerIP")), _userSettings.GetInt32("ServerPort"));
      }

      await _tcpClient.WriteAsync(dataBytes);
    }

    public event Action<IResponse> ReceiveData;

    private void StartReceivingData()
    {
      Task.Run(async () =>
      {
        while (_tcpClient.Connected)
        {
          var dataBytes = _tcpClient.ReadAsync();

//          await stream.ReadAsync();
        }
      });
    }
  }
}