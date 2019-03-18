namespace HomeAutomation.Server.Core
{
  public interface IHomeAutomationCommunication
  {
    byte[] HandleReceivedBytes(byte[] receivedBytes);
  }
}