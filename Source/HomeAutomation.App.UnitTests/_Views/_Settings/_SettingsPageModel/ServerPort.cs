using FluentAssertions;
using HomeAutomation.App.Views.Settings;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Settings._SettingsPageModel
{
  public class ServerPort
  {
    [Fact]
    public void ShouldSaveServerPortInUserSettings()
    {
      var userSettingsMock = new Mock<IUserSettings>();
      var pageModel = new SettingsPageModel(userSettingsMock.Object);

      pageModel.ServerPort = 1234;

      userSettingsMock.Verify(x => x.Set(It.Is<string>(y => y == "ServerPort"), It.Is<int>(y => y == 1234)));
    }

    [Fact]
    public void ShouldGetServerPortFromUserSettings()
    {
      var userSettingsMock = new Mock<IUserSettings>();
      userSettingsMock.Setup(x => x.GetInt32("ServerPort")).Returns(2345);
      var pageModel = new SettingsPageModel(userSettingsMock.Object);

      var serverPort = pageModel.ServerPort;

      serverPort.Should().Be(2345);
    }
  }
}