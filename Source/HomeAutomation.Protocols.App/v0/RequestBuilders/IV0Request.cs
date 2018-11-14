namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public interface IV0Request
  {
    byte RequestType0 { get; }
    byte RequestType1 { get; }
    byte RequestType2 { get; }
    byte RequestType3 { get; }
    byte[] Data { get; }
  }
}