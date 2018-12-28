namespace HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms
{
  public class GetRoomDescriptionRequestBuilder : RequestWithConnectBuilderBase, IGetRoomDescriptionRequestBuilder
  {
    public GetRoomDescriptionRequestBuilder(ICounter counter, IConnectionIdentification connectionIdentifier)
      : base(counter, connectionIdentifier)
    {}

    protected override byte RequestType0 => 0x02;
    protected override byte RequestType1 => 0x02;
    protected override byte RequestType2 => 0x00;
    protected override byte RequestType3 => 0x00;
  }
}