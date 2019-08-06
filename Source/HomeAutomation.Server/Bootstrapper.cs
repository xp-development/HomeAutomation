using System;
using System.Net.Sockets;
using Windows.Foundation;
using Grace.DependencyInjection;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.DataConverters;
using HomeAutomation.Server.Core;
using MetroLog;

namespace HomeAutomation.Server
{
  public sealed class Bootstrapper
  {
    private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<Bootstrapper>();
    private DependencyInjectionContainer _container;

    public IAsyncOperation<object> RunAsync()
    {
      _container = CreateContainer();
      ConfigureContainer();
      var tcpServer = _container.Locate<ITcpServer>();
      tcpServer.DataReceived += TcpServerOnDataReceived;
      return tcpServer.StartAsync().AsAsyncOperation();
    }

    private static DependencyInjectionContainer CreateContainer()
    {
      Log.Debug("Create container.");
      return new DependencyInjectionContainer();
    }

    private void ConfigureContainer()
    {
      Log.Debug("Configure container.");

      _container.Configure(c => c.Export<RequestParser>().As<IRequestParser>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ResponseBuilder>().As<IResponseBuilder>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<TcpServer>().As<ITcpServer>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<HomeAutomationCommunication>().As<IHomeAutomationCommunication>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ConnectionHandler>().As<IConnectionHandler>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<DataConverterDispatcher>().As<IDataConverterDispatcher>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ByteConverter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Int32ArrayConverter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<Int32Converter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<UInt16Converter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<StringConverter>().As<IDataConverter>().Lifestyle.Singleton());
      _container.Configure(c => c.ExportInstance(new ServiceLocator(_container)).As<IServiceLocator>().Lifestyle.Singleton());
    }

    private void TcpServerOnDataReceived(ITcpServer tcpServer, TcpClient tcpClient, byte[] dataBytes)
    {
      Log.Debug($"Received data {BitConverter.ToString(dataBytes)}.");
      tcpServer.SendData(tcpClient, _container.Locate<IHomeAutomationCommunication>().HandleReceivedBytes(dataBytes));
    }
  }
}