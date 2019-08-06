using Grace.DependencyInjection;
using HomeAutomation.App.Communication;
using HomeAutomation.App.DependencyInjection;
using HomeAutomation.App.Events;
using HomeAutomation.App.Views.Shell;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.App
{
  public class Bootstrapper
  {
    private DependencyInjectionContainer _container;

    public MainView Run()
    {
      ConfigureDependencies();
      HandleCommunication();
      return RunApp();
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
      _container.Configure(c => c.Export<ConnectionIdentification>().As<IConnectionIdentification>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ResponseParser>().As<IResponseParser>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<RequestBuilder>().As<IRequestBuilder>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<DataConverterDispatcher>().As<IDataConverterDispatcher>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ByteConverter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Int32ArrayConverter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Int32Converter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<UInt16Converter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<StringConverter>().As<IDataConverter>().Lifestyle.Singleton());
    }

    private void HandleCommunication()
    {
      _container.Locate<ICommunicator>().ReceiveData += OnReceiveData;
    }

    private void OnReceiveData(IResponse response)
    {
      if (response is ConnectDataResponse connectDataResponse)
      {
        _container.Locate<IConnectionIdentification>().Current = new[] { connectDataResponse.ConnectionIdentifier0, connectDataResponse.ConnectionIdentifier1, connectDataResponse.ConnectionIdentifier2, connectDataResponse.ConnectionIdentifier3 };
      }
    }

    private MainView RunApp()
    {
      return _container.Locate<MainView>();
    }
  }
}