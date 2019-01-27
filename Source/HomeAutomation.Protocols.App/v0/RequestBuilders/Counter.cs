using System;

namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public class Counter : ICounter
  {
    private static ushort _counter;

    public byte[] GetNext()
    {
      return BitConverter.GetBytes(++_counter);
    }
  }
}