using System;
using System.Net;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using IRequestBuilder = HomeAutomation.Protocols.App.v0.IRequestBuilder;

namespace HomeAutomation.App.Communication
{
  public class Communicator : ICommunicator
  {
    private readonly ITcpClient _tcpClient;
    private readonly IResponseParser _responseParser;
    private readonly IRequestBuilder _requestBuilder;
    private readonly IUserSettings _userSettings;
    private readonly IConnectionIdentification _connectionIdentification;

    public Communicator(IConnectionIdentification connectionIdentification, IUserSettings userSettings, ITcpClient tcpClient, IResponseParser responseParser, IRequestBuilder requestBuilder)
    {
      _tcpClient = tcpClient;
      _responseParser = responseParser;
      _requestBuilder = requestBuilder;
      _userSettings = userSettings;
      _connectionIdentification = connectionIdentification;
    }

    public async Task SendAsync(IRequest request)
    {
      if (!_tcpClient.Connected)
      {
        await _tcpClient.ConnectAsync(IPAddress.Parse(_userSettings.GetString("ServerIP")), _userSettings.GetInt32("ServerPort"));
        StartReceivingData();
      }

      if (request is IConnectionRequiredRequest connectionRequiredRequest)
      {
        connectionRequiredRequest.ConnectionIdentifier0 = _connectionIdentification.Current[0];
        connectionRequiredRequest.ConnectionIdentifier1 = _connectionIdentification.Current[1];
        connectionRequiredRequest.ConnectionIdentifier2 = _connectionIdentification.Current[2];
        connectionRequiredRequest.ConnectionIdentifier3 = _connectionIdentification.Current[3];
      }

      var requestBytes = _requestBuilder.Build(request);
      await _tcpClient.WriteAsync(requestBytes);
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
          ReceiveData?.Invoke(response);
        }
      });
    }
  }
}