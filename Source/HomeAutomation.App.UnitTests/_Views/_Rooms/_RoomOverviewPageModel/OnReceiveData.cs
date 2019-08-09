using FluentAssertions;
using HomeAutomation.App.Communication;
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

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsDataResponse{ RoomIdentifiers = new[] {1, 2, 3} });

      viewModel.Rooms.Count.Should().Be(3);
      viewModel.Rooms[0].Id.Should().Be(1);
      viewModel.Rooms[1].Id.Should().Be(2);
      viewModel.Rooms[2].Id.Should().Be(3);
    }

    [Fact]
    public async void ShouldSendGetRoomDescriptionRequestForAllRoomsOnReceiveGetAllRoomsResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsDataResponse { RoomIdentifiers = new[] {1, 2, 3} });

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 1)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 2)));
      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(y => y is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest)y).Identifier == 3)));
    }

    [Fact]
    public async void ShouldSetRoomDescriptionOnReceiveGetRoomDescriptionResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object);
      await viewModel.LoadedAsync(null);
      viewModel.Rooms.Add(new RoomViewModel(1));

      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse { RoomIdentifier = 1, Description = "living room" });

      viewModel.Rooms[0].Description.Should().Be("living room");
    }

    [Fact]
    public async void ShouldAddNewRoomOnReceiveCreateRoomDataResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object);
      await viewModel.LoadedAsync(null);
      await viewModel.NewRoomCommand.Execute(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new CreateRoomDataResponse { RoomIdentifier = 3, ClientRoomIdentifier = 0x01 });

      viewModel.Rooms.Should().HaveCount(1);
      viewModel.Rooms[0].Id.Should().Be(3);
      viewModel.Rooms[0].Description.Should().Be("New room");
    }
  }
}