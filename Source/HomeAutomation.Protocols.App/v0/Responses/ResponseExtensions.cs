namespace HomeAutomation.Protocols.App.v0.Responses
{
  public static class ResponseExtensions
  {
    public static bool IsNotConnectedResponse(this IResponse response)
    {
      return response.ResponseCode0 == 0xFF && response.ResponseCode1 == 0x02;
    }
  }
}