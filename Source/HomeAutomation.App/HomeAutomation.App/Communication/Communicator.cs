using System;
using System.Net;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.App.Communication
{
  public class Communicator : ICommunicator
  {
    private readonly ITcpClient _tcpClient;
    private readonly IResponseParser _responseParser;
    private readonly IUserSettings _userSettings;

    public Communicator(IUserSettings userSettings, ITcpClient tcpClient, IResponseParser responseParser)
    {
      _tcpClient = tcpClient;
      _responseParser = responseParser;
      _userSettings = userSettings;
    }

    public async Task SendAsync(byte[] dataBytes)
    {
      if (!_tcpClient.Connected)
      {
        await _tcpClient.ConnectAsync(IPAddress.Parse(_userSettings.GetString("ServerIP")), _userSettings.GetInt32("ServerPort"));
        StartReceivingData();
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
          var dataBytes = await _tcpClient.ReadAsync();

          var response = _responseParser.Parse(dataBytes);
//          Console.WriteLine(response.ResponseCode0 + "-" + response.ResponseCode1 + "-" + response.RequestType0 + "-" + response.RequestType1);
//          await stream.ReadAsync();
        }
      });
    }
  }
}