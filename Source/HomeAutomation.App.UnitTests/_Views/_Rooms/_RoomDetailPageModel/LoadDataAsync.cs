using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomDetailPageModel
{
  public class LoadDataAsync
  {
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(300)]
    public async void ShouldSendGetRoomDescriptionRequest(int roomId)
    {
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(roomId);

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(request => request is GetRoomDescriptionDataRequest && ((GetRoomDescriptionDataRequest) request).Identifier == roomId)));
    }
  }
}