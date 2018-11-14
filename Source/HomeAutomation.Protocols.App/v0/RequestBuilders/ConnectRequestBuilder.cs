namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public class ConnectRequestBuilder : V0BuilderBase
  {
    public ConnectRequestBuilder(ICounter counter)
      : base(counter)
    {
    }

    public override byte RequestType0 => 0x01;
    public override byte RequestType1 => 0x00;
    public override byte RequestType2 => 0x00;
    public override byte RequestType3 => 0x00;

    public override byte[] Data => new byte[0];
  }
}