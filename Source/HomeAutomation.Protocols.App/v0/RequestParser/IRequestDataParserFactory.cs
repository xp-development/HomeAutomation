namespace HomeAutomation.Protocols.App.v0.RequestParser
{
  public interface IRequestDataParserFactory
  {
    IRequestDataParser TryCreate(byte requestType0, byte requestType1, byte requestType2, byte requestType3);
  }
}