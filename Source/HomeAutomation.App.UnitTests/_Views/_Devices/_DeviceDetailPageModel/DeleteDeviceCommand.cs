using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Devices;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceDetailPageModel
{
  public class DeleteDeviceCommand
  {
    [Fact]
    public async void ShouldDeleteDevice()
    {
      const int deviceId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new DeviceDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(deviceId);
      communicatorMock.Raise(x => x.ReceiveData += null, new GetDeviceDescriptionDataResponse{ Identifier = deviceId, Description = "my device description" });

      await viewModel.DeleteDeviceCommand.Execute(null);

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(request => request is DeleteDeviceDataRequest 
                                                                          && ((DeleteDeviceDataRequest)request).Identifier == deviceId)));
    }
  }
}