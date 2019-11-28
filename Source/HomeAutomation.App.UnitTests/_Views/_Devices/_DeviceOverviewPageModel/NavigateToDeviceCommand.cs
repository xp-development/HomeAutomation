using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceOverviewPageModel
{
  public class NavigateToDeviceCommand
  {
    [Fact]
    public void ShouldNavigateToDevice()
    {
      const int deviceId = 3;
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new DeviceOverviewPageModel(new Mock<ICommunicator>().Object, eventAggregatorMock.Object);

      viewModel.NavigateToObjectCommand.Execute(deviceId);

      eventAggregatorMock.Verify(x => x.PublishAsync(It.Is<NavigationEvent>(y => y.ViewType == typeof(DeviceDetailPage) && (int) y.Parameter == deviceId)));
    }
  }
}