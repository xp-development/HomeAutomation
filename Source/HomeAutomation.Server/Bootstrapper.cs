using System;
using System.Net.Sockets;
using Windows.Foundation;
using Grace.DependencyInjection;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
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
      _container.Configure(c => c.Export<RequestDataParserFactory>().As<IRequestDataParserFactory>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ResponseBuilderDispatcher>().As<IResponseBuilderDispatcher>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<ConnectResponseBuilder>().As<IResponseBuilder>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<TcpServer>().As<ITcpServer>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<HomeAutomationCommunication>().As<IHomeAutomationCommunication>().Lifestyle.Singleton());
      _container.Configure(c => c.Export<CommonResponseCodeResponseBuilder>().As<ICommonResponseCodeResponseBuilder>().Lifestyle.Singleton());
    }

    private void TcpServerOnDataReceived(ITcpServer tcpServer, TcpClient tcpClient, byte[] dataBytes)
    {
      Log.Debug($"Received data {BitConverter.ToString(dataBytes)}.");
      tcpServer.SendData(tcpClient, _container.Locate<IHomeAutomationCommunication>().HandleReceivedBytes(dataBytes));
    }
  }
}