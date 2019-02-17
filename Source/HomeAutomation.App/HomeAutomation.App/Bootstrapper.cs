using Grace.DependencyInjection;
using HomeAutomation.App.Communication;
using HomeAutomation.App.DependencyInjection;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Shell;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms;

namespace HomeAutomation.App
{
  public class Bootstrapper
  {
    private DependencyInjectionContainer _container;

    public MainView Run()
    {
      ConfigureDependencies();
      return RunApp();
    }

    private MainView RunApp()
    {
      return _container.Locate<MainView>();
    }

    private void ConfigureDependencies()
    {
      _container = new DependencyInjectionContainer();

      ConfigureApplicationDependencies();
      ConfigureProtocolDependencies();

      StaticServiceLocator.SetCurrent(_container.Locate<IServiceLocator>());
    }

    private void ConfigureApplicationDependencies()
    {
      _container.Configure(c => c.Export<EventAggregator>().As<IEventAggregator>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Communicator>().As<ICommunicator>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<UserSettings>().As<IUserSettings>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<TcpClient>().As<ITcpClient>());
      _container.Configure(c => c.ExportFactory<IServiceLocator>(() => new ServiceLocator(_container)).Lifestyle.Singleton());
    }

    private void ConfigureProtocolDependencies()
    {
      _container.Configure(c => c.Export<GetAllRoomsRequestBuilder>().As<IGetAllRoomsRequestBuilder>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<GetRoomDescriptionRequestBuilder>().As<IGetRoomDescriptionRequestBuilder>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Counter>().As<ICounter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ConnectionIdentification>().As<IConnectionIdentification>().Lifestyle.Singleton());
    }
  }
}