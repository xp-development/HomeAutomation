using FluentAssertions;
using HomeAutomation.App.Communication;
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

      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, getAllRoomsRequestBuilderMock.Object);
      await viewModel.LoadedAsync(null);

      communicatorMock.Raise(x => x.ReceiveData += null, new GetAllRoomsResponse(0, 1, 0, 0, new[] {1, 2, 3}));

      viewModel.Rooms.Count.Should().Be(3);
      viewModel.Rooms[0].Id.Should().Be(1);
      viewModel.Rooms[1].Id.Should().Be(2);
      viewModel.Rooms[2].Id.Should().Be(3);
    }
  }
}