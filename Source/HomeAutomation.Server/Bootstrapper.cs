using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.Sockets;
using Grace.DependencyInjection;
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
          await streamSocketListener.BindServiceNameAsync("42123");
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
      byte[] dataBytes;
      using (var reader = new BinaryReader(args.Socket.InputStream.AsStreamForRead()))
      {
        dataBytes = reader.ReadBytes(int.MaxValue);
      }

      Log.Debug($"Received data {BitConverter.ToString(dataBytes)}.");

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