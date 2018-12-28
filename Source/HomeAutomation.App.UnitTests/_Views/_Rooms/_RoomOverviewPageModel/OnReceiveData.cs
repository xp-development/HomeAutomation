using FluentAssertions;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Models;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms;
using HomeAutomation.Protocols.App.v0.ResponseParsers.Rooms;
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
      var getAllRoomsRequestBuilderMock = new Mock<IGetAllRoomsRequestBuilder>();
      var getRoomDescriptionRequestBuilderMock = new Mock<IGetRoomDescriptionRequestBuilder>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, getAllRoomsRequestBuilderMock.Object, getRoomDescriptionRequestBuilderMock.Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsResponse(0, 1, 0, 0, new[] {1, 2, 3}));

      viewModel.Rooms.Count.Should().Be(3);
      viewModel.Rooms[0].Id.Should().Be(1);
      viewModel.Rooms[1].Id.Should().Be(2);
      viewModel.Rooms[2].Id.Should().Be(3);
    }

    [Fact]
    public async void ShouldSendGetRoomDescriptionRequestForAllRoomsOnReceiveGetAllRoomsResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();
      var getAllRoomsRequestBuilderMock = new Mock<IGetAllRoomsRequestBuilder>();
      var getRoomDescriptionRequestBuilderMock = new Mock<IGetRoomDescriptionRequestBuilder>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, getAllRoomsRequestBuilderMock.Object, getRoomDescriptionRequestBuilderMock.Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsResponse(0, 1, 0, 0, new[] {1, 2, 3}));

      getRoomDescriptionRequestBuilderMock.Verify(x => x.Build(1));
      getRoomDescriptionRequestBuilderMock.Verify(x => x.Build(2));
      getRoomDescriptionRequestBuilderMock.Verify(x => x.Build(3));
    }

    [Fact]
    public async void ShouldSetRoomDescriptionOnReceiveGetRoomDescriptionResponse()
    {
      var communicatorMock = new Mock<ICommunicator>();
      var getAllRoomsRequestBuilderMock = new Mock<IGetAllRoomsRequestBuilder>();
      var getRoomDescriptionRequestBuilderMock = new Mock<IGetRoomDescriptionRequestBuilder>();

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, getAllRoomsRequestBuilderMock.Object, getRoomDescriptionRequestBuilderMock.Object);
      await viewModel.LoadedAsync(null);
      viewModel.Rooms.Add(new RoomViewModel(1));

      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionResponse(0, 1, 0, 0, 1, "living room"));

      viewModel.Rooms[0].Description.Should().Be("living room");
    }
  }
}