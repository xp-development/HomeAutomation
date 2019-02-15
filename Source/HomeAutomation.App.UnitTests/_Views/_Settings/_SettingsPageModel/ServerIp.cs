using FluentAssertions;
using HomeAutomation.App.Views.Settings;
using Moq;
using Xunit;

namespace HomeAutomation.App.UnitTests._Views._Settings._SettingsPageModel
{
  public class ServerIp
  {
    [Fact]
    public void ShouldSaveServerIpInUserSettings()
    {
      var userSettingsMock = new Mock<IUserSettings>();
      var pageModel = new SettingsPageModel(userSettingsMock.Object);

      pageModel.ServerIp = "123.123.123.123";

      userSettingsMock.Verify(x => x.Set(It.Is<string>(y => y == "ServerIP"), It.Is<string>(y => y == "123.123.123.123")));
    }

    [Fact]
    public void ShouldGetServerIpFromUserSettings()
    {
      var userSettingsMock = new Mock<IUserSettings>();
      userSettingsMock.Setup(x => x.GetString("ServerIP")).Returns("12.34.56.78");
      var pageModel = new SettingsPageModel(userSettingsMock.Object);

      var serverIp = pageModel.ServerIp;

      serverIp.Should().Be("12.34.56.78");
    }
  }
}