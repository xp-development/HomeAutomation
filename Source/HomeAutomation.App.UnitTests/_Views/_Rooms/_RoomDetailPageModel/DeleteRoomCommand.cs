using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using HomeAutomation.Protocols.App.v0.Responses.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomDetailPageModel
{
  public class DeleteRoomCommand
  {
    [Fact]
    public async void ShouldDeleteRoom()
    {
      const int roomId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(roomId);
      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse{ Identifier = roomId, Description = "my room description" });

      await viewModel.DeleteRoomCommand.Execute(null);

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(request => request is DeleteRoomDataRequest 
                                                                          && ((DeleteRoomDataRequest)request).RoomIdentifier == roomId)));
    }
  }
}