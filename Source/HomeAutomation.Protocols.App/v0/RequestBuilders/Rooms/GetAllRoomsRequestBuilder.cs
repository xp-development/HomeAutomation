namespace HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms
{
  public class GetAllRoomsRequestBuilder : RequestWithConnectBuilderBase, IGetAllRoomsRequestBuilder
  {
    public GetAllRoomsRequestBuilder(ICounter counter, IConnectionIdentification connectionIdentifier)
      : base(counter, connectionIdentifier)
    {}

    protected override byte RequestType0 => 0x02;
    protected override byte RequestType1 => 0x01;
    protected override byte RequestType2 => 0x00;
    protected override byte RequestType3 => 0x00;
    protected override byte[] Data => new byte[0];
  }
}