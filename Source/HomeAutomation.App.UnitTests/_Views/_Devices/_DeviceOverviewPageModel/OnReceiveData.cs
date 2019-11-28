using FluentAssertions;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.App.Views.Devices;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Devices._DeviceOverviewPageModel
{
  public class OnReceiveData
  {
    [Fact]
    public async void ShouldResetDevicesViewModelOnReceiveGetAllDevicesResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new DeviceOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllDevicesDataResponse{ Identifiers = new[] {1, 2, 3} });

      viewModel.Objects.Should().HaveCount(3);
      viewModel.Objects[0].Id.Should().Be(1);
      viewModel.Objects[1].Id.Should().Be(2);
      viewModel.Objects[2].Id.Should().Be(3);
    }

    [Fact]
    public async void ShouldSendGetDeviceDescriptionRequestForAllDevicesOnReceiveGetAllDevicesResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new DeviceOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllDevicesDataResponse { Identifiers = new[] {1, 2, 3} });

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetDeviceDescriptionDataRequest && ((GetDeviceDescriptionDataRequest)y).Identifier == 1)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetDeviceDescriptionDataRequest && ((GetDeviceDescriptionDataRequest)y).Identifier == 2)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetDeviceDescriptionDataRequest && ((GetDeviceDescriptionDataRequest)y).Identifier == 3)));
    }

    [Fact]
    public async void ShouldSetDeviceDescriptionOnReceiveGetDeviceDescriptionResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new DeviceOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);
      viewModel.Objects.Add(new DeviceViewModel {Id = 1});

      communicatorMock.Raise(x => x.ReceiveData += null, new GetDeviceDescriptionDataResponse { Identifier = 1, Description = "Airing" });

      viewModel.Objects[0].Description.Should().Be("Airing");
    }

    [Fact]
    public async void ShouldAddNewDeviceOnReceiveCreateDeviceDataResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new DeviceOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);
      await viewModel.NewObjectCommand.Execute(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new CreateDeviceDataResponse { Identifier = 3, ClientObjectIdentifier = 0x01 });

      viewModel.Objects.Should().HaveCount(1);
      viewModel.Objects[0].Id.Should().Be(3);
      viewModel.Objects[0].Description.Should().Be("New device");
    }
  }
}