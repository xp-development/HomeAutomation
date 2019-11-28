using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Devices;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceOverviewPageModel
{
  public class NewDeviceCommand
  {
    [Fact]
    public void ShouldCreateDeviceRoom()
    {
      var communicatorMock = new Mock<ICommunicator>();
      var viewModel = new DeviceOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);

      viewModel.NewObjectCommand.Execute(null);

      communicatorMock.Verify(x => x.SendAsync(It.Is<CreateDeviceDataRequest>(y => y.ClientObjectIdentifier == 0x01 && y.Description == "New device")));
    }
  }
}