namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IV0ResponseDataParser
  {
    TResponse Parse<TResponse>(byte protocolVersion, byte requestType0, byte requestType1, byte requestType2, byte requestType3, ushort counter, byte responseCode0, byte responseCode1, byte[] data)
      where TResponse : IResponseDataParser;
  }
}