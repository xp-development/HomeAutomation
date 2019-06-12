namespace HomeAutomation.Protocols.App.v0
{
  public interface ICommonResponseCodeResponseBuilder
  {
    byte[] Build(byte[] requestBytes, CommonResponseCode commonResponseCode);
  }
}