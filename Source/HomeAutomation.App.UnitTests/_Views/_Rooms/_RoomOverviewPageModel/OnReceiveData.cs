using FluentAssertions;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomOverviewPageModel
{
  public class OnReceiveData
  {
    [Fact]
    public async void ShouldResetRoomsViewModelOnReceiveGetAllRoomsResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsDataResponse{ Identifiers = new[] {1, 2, 3} });

      viewModel.Objects.Count.Should().Be(3);
      viewModel.Objects[0].Id.Should().Be(1);
      viewModel.Objects[1].Id.Should().Be(2);
      viewModel.Objects[2].Id.Should().Be(3);
    }

    [Fact]
    public async void ShouldSendGetRoomDescriptionRequestForAllRoomsOnReceiveGetAllRoomsResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsDataResponse { Identifiers = new[] {1, 2, 3} });

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 1)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 2)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 3)));
    }

    [Fact]
    public async void ShouldSetRoomDescriptionOnReceiveGetRoomDescriptionResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);
      viewModel.Objects.Add(new RoomViewModel {Id = 1});

      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse { Identifier = 1, Description = "living room" });

      viewModel.Objects[0].Description.Should().Be("living room");
    }

    [Fact]
    public async void ShouldAddNewRoomOnReceiveCreateRoomDataResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);
      await viewModel.LoadedAsync(null);
      await viewModel.NewObjectCommand.Execute(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new CreateRoomDataResponse { Identifier = 3, ClientObjectIdentifier = 0x01 });

      viewModel.Objects.Should().HaveCount(1);
      viewModel.Objects[0].Id.Should().Be(3);
      viewModel.Objects[0].Description.Should().Be("New room");
    }
  }
}