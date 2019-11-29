using FluentAssertions;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceDetailPageModel
{
  public class OnReceiveData
  {
    [Fact]
    public async void ShouldSetDevice()
    {
      const int deviceId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new DeviceDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(deviceId);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetDeviceDescriptionDataResponse{ Identifier = deviceId, Description = "my device description" });

      viewModel.Device.Id.Should().Be(deviceId);
      viewModel.Device.Description.Should().Be("my device description");
    }

    [Fact]
    public async void ShouldNavigateToDeviceOverviewIfDeviceWasDeleted()
    {
      const int deviceId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new DeviceDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(deviceId);
      communicatorMock.Raise(x => x.ReceiveData += null, new GetDeviceDescriptionDataResponse{ Identifier = deviceId, Description = "my device description" });

      communicatorMock.Raise(x => x.ReceiveData += null, new DeleteDeviceDataResponse{ Identifier = deviceId });

      eventAggregatorMock.Verify(x => x.PublishAsync(It.Is<NavigationEvent>(y => y.ViewType == typeof(DeviceOverviewPage))));
    }
  }
}