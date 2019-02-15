using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.Sockets;
using Grace.DependencyInjection;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestParsers;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
using MetroLog;

namespace HomeAutomation.Server
{
  public sealed class Bootstrapper
  {
    private DependencyInjectionContainer _container;
    private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<Bootstrapper>();

    public IAsyncOperation<object> RunAsync()
    {
      _container = CreateContainer();
      ConfigureContainer();
      return StartServerAsync();
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
    }

    private IAsyncOperation<object> StartServerAsync()
    {
      Log.Debug("Start server async.");
      return Task.Run<object>(async () =>
      {
        try
        {
          var streamSocketListener = new StreamSocketListener();
          streamSocketListener.ConnectionReceived += ConnectionReceived;
          await streamSocketListener.BindEndpointAsync(null, "42123");
        }
        catch (Exception ex)
        {
          Log.Error("Cannot initialize StreamSocketListener.", ex);
        }

        return null;
      }).AsAsyncOperation();
    }

    private void ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    {
      Log.Info("Receive connection.");
      var requestParser = _container.Locate<IRequestParser>();
      var responseBuilderDispatcher = _container.Locate<IResponseBuilderDispatcher>();

      while (true)
      {
        byte[] dataBytes;
        using (var reader = new BinaryReader(args.Socket.InputStream.AsStreamForRead()))
        {
          dataBytes = reader.ReadBytes(int.MaxValue);
        }

        Log.Debug($"Received data {BitConverter.ToString(dataBytes)}.");

        var bytes = responseBuilderDispatcher.Build(requestParser.Parse(dataBytes));
        using (var outputStream = args.Socket.OutputStream.AsStreamForWrite())
        {
          using (var streamWriter = new BinaryWriter(outputStream))
          {
            streamWriter.Write(bytes);
            streamWriter.Flush();
          }
        }
      }
    }
  }
}