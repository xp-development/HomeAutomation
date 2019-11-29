using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Devices;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceDetailPageModel
{
  public class LoadDataAsync
  {
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(300)]
    public async void ShouldSendGetDeviceDescriptionRequest(int deviceId)
    {
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new DeviceDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(deviceId);

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(request => request is GetDeviceDescriptionDataRequest && ((GetDeviceDescriptionDataRequest) request).Identifier == deviceId)));
    }
  }
}