using System.Collections.Generic;

namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public abstract class RequestWithConnectBuilderBase : RequestBuilderBase
  {
    public byte ConnectionIdentifier0 { get; }
    public byte ConnectionIdentifier1 { get; }
    public byte ConnectionIdentifier2 { get; }
    public byte ConnectionIdentifier3 { get; }

    protected RequestWithConnectBuilderBase(ICounter counter, IConnectionIdentification connectionIdentifier)
      : base(counter)
    {
      ConnectionIdentifier0 = connectionIdentifier.Current[0];
      ConnectionIdentifier1 = connectionIdentifier.Current[1];
      ConnectionIdentifier2 = connectionIdentifier.Current[2];
      ConnectionIdentifier3 = connectionIdentifier.Current[3];
    }

    protected override List<byte> OnBuild()
    {
      var bytes = base.OnBuild();
      bytes.InsertRange(7, new []{ ConnectionIdentifier0, ConnectionIdentifier1, ConnectionIdentifier2, ConnectionIdentifier3 });
      return bytes;
    }
  }
}