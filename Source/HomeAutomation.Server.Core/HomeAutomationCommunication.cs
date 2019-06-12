using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Server.Core.RequestHandlers;

namespace HomeAutomation.Server.Core
{
  public class HomeAutomationCommunication : IHomeAutomationCommunication
  {
    private readonly IRequestParser _requestParser;
    private readonly IResponseBuilder _responseBuilder;
    private readonly ICommonResponseCodeResponseBuilder _commonResponseCodeResponseBuilder;
    private readonly IDictionary<RequestTypeAttribute, Type> _requestHandlerTypes;
    private readonly IServiceLocator _serviceLocator;

    public HomeAutomationCommunication(IRequestParser requestParser, IResponseBuilder responseBuilder, ICommonResponseCodeResponseBuilder commonResponseCodeResponseBuilder, IServiceLocator serviceLocator)
    {
      _requestParser = requestParser;
      _responseBuilder = responseBuilder;
      _requestHandlerTypes = GetRequestHandlerTypes();
      _commonResponseCodeResponseBuilder = commonResponseCodeResponseBuilder;
      _serviceLocator = serviceLocator;
    }

    public byte[] HandleReceivedBytes(byte[] receivedBytes)
    {
      try
      {
        var request = _requestParser.Parse(receivedBytes);
        if (request is IConnectionRequiredRequest)
        {
          return _commonResponseCodeResponseBuilder.Build(receivedBytes, CommonResponseCode.NotConnected);
        }

        var requestType = (RequestTypeAttribute)request.GetType().GetCustomAttributes(typeof(RequestTypeAttribute), true)[0];
        if (!_requestHandlerTypes.TryGetValue(requestType, out var requestHandlerType))
          return _commonResponseCodeResponseBuilder.Build(receivedBytes, CommonResponseCode.UnknownCommand);

        var requestHandler = (IRequestHandler)Activator.CreateInstance(requestHandlerType);
        return _responseBuilder.Build(requestHandler.Handle(request));
      }
      catch (UnknownCommandException)
      {
        return _commonResponseCodeResponseBuilder.Build(receivedBytes, CommonResponseCode.UnknownCommand);
      }
      catch (WrongCrcException)
      {
        return _commonResponseCodeResponseBuilder.Build(receivedBytes, CommonResponseCode.WrongCrc);
      }
      catch (NotSupportedProtocolException)
      {
        return _commonResponseCodeResponseBuilder.Build(receivedBytes, CommonResponseCode.NotSupportedProtocolVersion);
      }
    }

    private static IDictionary<RequestTypeAttribute, Type> GetRequestHandlerTypes()
    {
      return typeof(HomeAutomationCommunication).Assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(RequestTypeAttribute), true).Length > 0).Where(type => type.GetInterface(nameof(IRequestHandler)) != null).ToDictionary(x => (RequestTypeAttribute)x.GetCustomAttributes(typeof(RequestTypeAttribute), true)[0], x => x);
    }
  }
}