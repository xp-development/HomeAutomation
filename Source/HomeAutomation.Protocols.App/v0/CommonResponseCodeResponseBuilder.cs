using System;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0
{
  public class CommonResponseCodeResponseBuilder : ICommonResponseCodeResponseBuilder
  {
    public byte[] Build(byte[] requestBytes, CommonResponseCode commonResponseCode)
    {
      var responseBytes = requestBytes.ToList();
      var commonResponseCodeBytes = BitConverter.GetBytes((ushort) commonResponseCode).Reverse();
      responseBytes.InsertRange(7, commonResponseCodeBytes);
      var checksumBytes = Crc16.ComputeChecksum(responseBytes.Take(responseBytes.Count - 2));
      responseBytes[responseBytes.Count - 2] = checksumBytes[0];
      responseBytes[responseBytes.Count - 1] = checksumBytes[1];
      return responseBytes.ToArray();
    }
  }
}