using FluentAssertions;
using HomeAutomation.App.Communication;
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
      var viewModel = new RoomDetailPageModel(communicatorMock.Object);
      await viewModel.LoadedAsync(roomId);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse{ RoomIdentifier = roomId, Description = "my room description" });

      viewModel.Room.Id.Should().Be(roomId);
      viewModel.Room.Description.Should().Be("my room description");
    }
  }
}