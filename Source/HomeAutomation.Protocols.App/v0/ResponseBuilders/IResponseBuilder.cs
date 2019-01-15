using System;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.ResponseBuilders
{
  public interface IResponseBuilder
  {
    byte[] Build(IRequest request);
    Type RequestType { get; }
  }
}