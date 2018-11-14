using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0
{
  public static class Crc16
  {
    private const ushort Polynomial = 0xA001;
    private static readonly ushort[] Table = new ushort[256];

    public static byte[] ComputeChecksum(IEnumerable<byte> bytes)
    {
      var computeChecksum = bytes.Aggregate<byte, ushort>(0, (current, b) => (ushort) ((current >> 8) ^ Table[(byte) (current ^ b)]));
      return BitConverter.GetBytes(computeChecksum);
    }

    static Crc16()
    {
      for (ushort i = 0; i < Table.Length; ++i)
      {
        ushort value = 0;
        var temp = i;
        for (byte j = 0; j < 8; ++j)
        {
          if (((value ^ temp) & 0x0001) != 0)
            value = (ushort)((value >> 1) ^ Polynomial);
          else
            value >>= 1;

          temp >>= 1;
        }

        Table[i] = value;
      }
    }
  }
}