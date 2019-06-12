using System;
using System.Threading.Tasks;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.App.Communication
{
  public interface ICommunicator
  {
    Task SendAsync(IRequest request);
    event Action<IResponse> ReceiveData;
  }
}