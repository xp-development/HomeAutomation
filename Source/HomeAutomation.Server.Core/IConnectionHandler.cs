namespace HomeAutomation.Server.Core
{
  public interface IConnectionHandler
  {
    byte[] NewConnection();
    bool IsConnected(byte[] connectionIdentifiers);
  }
}