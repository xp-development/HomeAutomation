using Grace.DependencyInjection;
using HomeAutomation.App.Communication;
using HomeAutomation.App.DependencyInjection;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Shell;

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

      _container.Configure(c => c.Export<EventAggregator>().As<IEventAggregator>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Communicator>().As<ICommunicator>().Lifestyle.Singleton());
      _container.Configure(c => c.ExportFactory<IServiceLocator>(() => new ServiceLocator(_container)).Lifestyle.Singleton());

      StaticServiceLocator.SetCurrent(_container.Locate<IServiceLocator>());
    }
  }
}