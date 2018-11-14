namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public interface IResponseDataParserFactory
  {
    IResponseDataParser TryCreate(byte requestType0, byte requestType1, byte requestType2, byte requestType3);

    void Register<TResponseDataParser>()
      where TResponseDataParser : IResponseDataParser;
  }
}