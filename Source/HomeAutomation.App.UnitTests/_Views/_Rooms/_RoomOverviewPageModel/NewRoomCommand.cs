using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Rooms;
using HomeAutomation.Protocols.App.v0.Requests.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomOverviewPageModel
{
  public class NewRoomCommand
  {
    [Fact]
    public void ShouldCreateNewRoom()
    {
      var communicatorMock = new Mock<ICommunicator>();
      var viewModel = new RoomOverviewPageModel(communicatorMock.Object, new Mock<IEventAggregator>().Object);

      viewModel.NewObjectCommand.Execute(null);

      communicatorMock.Verify(x => x.SendAsync(It.Is<CreateRoomDataRequest>(y => y.ClientObjectIdentifier == 0x01 && y.Description == "New room")));
    }
  }
}