using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Server.Core.RequestHandlers;

namespace HomeAutomation.Server.Core
{
  public class HomeAutomationCommunication : IHomeAutomationCommunication
  {
    private readonly IRequestParser _requestParser;
    private readonly IResponseBuilder _responseBuilder;
    private readonly IDictionary<RequestTypeAttribute, Type> _requestHandlerTypes;
    private readonly IConnectionHandler _connectionHandler;
    private readonly IServiceLocator _serviceLocator;

    public HomeAutomationCommunication(IRequestParser requestParser, IResponseBuilder responseBuilder, IConnectionHandler connectionHandler, IServiceLocator serviceLocator)
    {
      _requestParser = requestParser;
      _responseBuilder = responseBuilder;
      _requestHandlerTypes = GetRequestHandlerTypes();
      _connectionHandler = connectionHandler;
      _serviceLocator = serviceLocator;
    }

    public byte[] HandleReceivedBytes(byte[] receivedBytes)
    {
      var requestType0 = receivedBytes[1];
      var requestType1 = receivedBytes[2];
      var requestType2 = receivedBytes[3];
      var requestType3 = receivedBytes[4];
      var counter = BitConverter.ToUInt16(receivedBytes, 7);
      IResponse response;
      try
      {
        var request = _requestParser.Parse(receivedBytes);
        var requestType = (RequestTypeAttribute) request.GetType().GetCustomAttributes(typeof(RequestTypeAttribute), true)[0];
        if (request is IConnectionRequiredRequest connectionRequiredRequest && !_connectionHandler.IsConnected(new[] { connectionRequiredRequest.ConnectionIdentifier0, connectionRequiredRequest.ConnectionIdentifier1, connectionRequiredRequest.ConnectionIdentifier2, connectionRequiredRequest.ConnectionIdentifier3 }))
          response = new CommonErrorResponse(CommonResponseCode.NotConnected, counter, requestType0, requestType1, requestType2, requestType3);
        else if (!_requestHandlerTypes.TryGetValue(requestType, out var requestHandlerType))
          response = new CommonErrorResponse(CommonResponseCode.UnknownCommand, counter, requestType0, requestType1, requestType2, requestType3);
        else
        {
          var requestHandler = (IRequestHandler) _serviceLocator.Locate(requestHandlerType);
          response = requestHandler.Handle(request);
        }
      }
      catch (UnknownCommandException)
      {
        response = new CommonErrorResponse(CommonResponseCode.UnknownCommand, counter, requestType0, requestType1, requestType2, requestType3);
      }
      catch (WrongCrcException)
      {
        response = new CommonErrorResponse(CommonResponseCode.WrongCrc, counter, requestType0, requestType1, requestType2, requestType3);
      }
      catch (NotSupportedProtocolException)
      {
        response = new CommonErrorResponse(CommonResponseCode.NotSupportedProtocolVersion, counter, requestType0, requestType1, requestType2, requestType3);
      }

      response.Counter = counter;
      return _responseBuilder.Build(response);
    }

    private static IDictionary<RequestTypeAttribute, Type> GetRequestHandlerTypes()
    {
      return typeof(HomeAutomationCommunication).Assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(RequestTypeAttribute), true).Length > 0).Where(type => type.GetInterface(nameof(IRequestHandler)) != null).ToDictionary(x => (RequestTypeAttribute)x.GetCustomAttributes(typeof(RequestTypeAttribute), true)[0], x => x);
    }
  }
}