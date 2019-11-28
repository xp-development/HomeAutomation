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
  public class SaveRoomCommand
  {
    [Fact]
    public async void ShouldRenameRoom()
    {
      const int roomId = 5;
      var communicatorMock = new Mock<ICommunicator>();
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomDetailPageModel(communicatorMock.Object, eventAggregatorMock.Object);
      await viewModel.LoadedAsync(roomId);
      communicatorMock.Raise(x => x.ReceiveData += null, new GetRoomDescriptionDataResponse{ Identifier = roomId, Description = "my room description" });
      viewModel.Room.Description = "new room description";

      await viewModel.SaveRoomCommand.Execute(null);

      communicatorMock.Verify(x => x.SendAsync(It.Is<IRequest>(request => request is RenameRoomDescriptionDataRequest 
                                                                          && ((RenameRoomDescriptionDataRequest)request).Identifier == roomId
                                                                          && ((RenameRoomDescriptionDataRequest)request).Description == viewModel.Room.Description)));
    }
  }
}