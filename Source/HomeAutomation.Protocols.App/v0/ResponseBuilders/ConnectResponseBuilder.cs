using System;
using System.Collections.Generic;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using HomeAutomation.Protocols.App.v0.RequestParsers;

namespace HomeAutomation.Protocols.App.v0.ResponseBuilders
{
  public class ConnectResponseBuilder
    : ResponseBuilderBase<ConnectRequest>
  {
    private static uint _connectionIdentifier;

    protected override IEnumerable<byte> OnBuild(IRequest request)
    {
      yield return 0x00; //response code 0
      yield return 0x00; //response code 1
      yield return 0x04; //data length
      yield return 0x00; //data length

      var connectionIdentifierBytes = BitConverter.GetBytes(++_connectionIdentifier);
      yield return connectionIdentifierBytes[0];
      yield return connectionIdentifierBytes[1];
      yield return connectionIdentifierBytes[2];
      yield return connectionIdentifierBytes[3];
    }
  }
}