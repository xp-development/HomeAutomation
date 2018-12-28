using System;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.ResponseParsers;

namespace HomeAutomation.App.Communication
{
  public interface ICommunicator
  {
    Task SendAsync(byte[] dataBytes);
    event Action<IResponse> ReceiveData;
  }
}