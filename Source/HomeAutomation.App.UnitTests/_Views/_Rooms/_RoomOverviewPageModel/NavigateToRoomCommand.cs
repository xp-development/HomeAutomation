using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Rooms._RoomOverviewPageModel
{
  public class NavigateToRoomCommand
  {
    [Fact]
    public void ShouldCreateNewRoom()
    {
      const int roomId = 3;
      var eventAggregatorMock = new Mock<IEventAggregator>();
      var viewModel = new RoomOverviewPageModel(new Mock<ICommunicator>().Object, eventAggregatorMock.Object);

      viewModel.NavigateToRoomCommand.Execute(roomId);

      eventAggregatorMock.Verify(x => x.PublishAsync(It.Is<NavigationEvent>(y => y.ViewType == typeof(RoomDetailPage) && (int) y.Parameter == roomId)));
    }
  }
}