namespace HomeAutomation.Protocols.App.v0.ResponseBuilders
{
  public interface ICommonResponseCodeResponseBuilder
  {
    byte[] Build(byte[] requestBytes, CommonResponseCode commonResponseCode);
  }
}