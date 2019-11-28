using FluentAssertions;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomDetailPageModel
{
  public class OnReceiveData
  {
    [Fact]
    public async void ShouldSetRoom()
    {
      const int roomId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(roomId);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse{ Identifier = roomId, Description = "my room description" });

      viewModel.Room.Id.Should().Be(roomId);
      viewModel.Room.Description.Should().Be("my room description");
    }

    [Fact]
    public async void ShouldNavigateToRoomOverviewIfRoomWasDeleted()
    {
      const int roomId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(roomId);
      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse{ Identifier = roomId, Description = "my room description" });

      communicatorMock.Raise(x => x.ReceiveData += null, new DeleteRoomDataResponse{ RoomIdentifier = roomId });

      eventAggregatorMock.Verify(x => x.PublishAsync(It.Is<NavigationEvent>(y => y.ViewType == typeof(RoomOverviewPage))));
    }
  }
}