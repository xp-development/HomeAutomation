using System;
using System.Collections.Generic;

namespace HomeAutomation.Server.Core
{
  public class ConnectionHandler : IConnectionHandler
  {
    private uint _currentIdentifier;
    private readonly HashSet<uint> _connections = new HashSet<uint>();

    public byte[] NewConnection()
    {
      ++_currentIdentifier;
      _connections.Add(_currentIdentifier);
      return BitConverter.GetBytes(_currentIdentifier);
    }

    public bool IsConnected(byte[] connectionIdentifiers)
    {
      return _connections.Contains(BitConverter.ToUInt32(connectionIdentifiers, 0));
    }
  }
}