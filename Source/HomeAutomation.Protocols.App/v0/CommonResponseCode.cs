namespace HomeAutomation.Protocols.App.v0
{
  public enum CommonResponseCode : ushort
  {
    WrongCrc = 0xFF01,
    NotConnected = 0xFF02,
    TransactionRequired = 0xFF03,
    UnknownCommand = 0xFF04,
    CorruptData = 0xFF05,
    NotSupportedProtocolVersion = 0xFF06
  }
}