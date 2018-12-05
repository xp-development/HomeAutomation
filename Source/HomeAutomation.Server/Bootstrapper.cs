using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.Sockets;
using Grace.DependencyInjection;
using NLog;

namespace HomeAutomation.Server
{
  public sealed class Bootstrapper
  {
    private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
    private DependencyInjectionContainer _container;

    public IAsyncOperation<object> RunAsync()
    {
      _container = CreateContainer();
      ConfigureContainer();
      return StartServerAsync();
    }

    private static DependencyInjectionContainer CreateContainer()
    {
      _log.Debug("Create container.");
      return new DependencyInjectionContainer();
    }

    private void ConfigureContainer()
    {
      _log.Debug("Configure container.");
    }

    private IAsyncOperation<object> StartServerAsync()
    {
      _log.Debug("Start server async.");
      return Task.Run<object>(async () =>
      {
        try
        {
          var streamSocketListener = new StreamSocketListener();
          streamSocketListener.ConnectionReceived += ConnectionReceived;
          await streamSocketListener.BindServiceNameAsync("42123");
        }
        catch (Exception ex)
        {
          _log.Error(ex, "Cannot initialize StreamSocketListener.");
        }

        return null;
      }).AsAsyncOperation();
    }

    private void ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    {
      _log.Info("Receive connection.");
      byte[] dataBytes;
      using (var reader = new BinaryReader(args.Socket.InputStream.AsStreamForRead()))
      {
        dataBytes = reader.ReadBytes(int.MaxValue);
      }

      _log.Debug($"Received data {BitConverter.ToString(dataBytes)}.");

      using (var outputStream = args.Socket.OutputStream.AsStreamForWrite())
      {
        using (var streamWriter = new BinaryWriter(outputStream))
        {
          streamWriter.Write(new byte[0]);
          streamWriter.Flush();
        }
      }
    }
  }
}