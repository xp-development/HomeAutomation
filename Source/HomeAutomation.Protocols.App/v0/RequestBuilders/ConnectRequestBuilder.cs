namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public class ConnectRequestBuilder : RequestBuilderBase
  {
    public ConnectRequestBuilder(ICounter counter)
      : base(counter)
    {
    }

    protected override byte RequestType0 => 0x01;
    protected override byte RequestType1 => 0x00;
    protected override byte RequestType2 => 0x00;
    protected override byte RequestType3 => 0x00;
  }
}