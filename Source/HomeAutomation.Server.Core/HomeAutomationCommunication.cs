using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;

namespace HomeAutomation.Server.Core
{
  public class HomeAutomationCommunication : IHomeAutomationCommunication
  {
    private readonly IRequestParser _requestParser;
    private readonly IResponseBuilderDispatcher _responseBuilderDispatcher;
    private readonly ICommonResponseCodeResponseBuilder _commonResponseCodeResponseBuilder;

    public HomeAutomationCommunication(IRequestParser requestParser, IResponseBuilderDispatcher responseBuilderDispatcher, ICommonResponseCodeResponseBuilder commonResponseCodeResponseBuilder)
    {
      _requestParser = requestParser;
      _responseBuilderDispatcher = responseBuilderDispatcher;
      _commonResponseCodeResponseBuilder = commonResponseCodeResponseBuilder;
    }

    public byte[] HandleReceivedBytes(byte[] receivedBytes)
    {
      try
      {
        var request = _requestParser.Parse(receivedBytes);
        return _responseBuilderDispatcher.Build(request);
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
  }
}