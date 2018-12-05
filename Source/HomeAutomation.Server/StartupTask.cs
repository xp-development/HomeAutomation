using System;
using System.Reflection;
using Windows.ApplicationModel.Background;
using NLog;

namespace HomeAutomation.Server
{
  public sealed class StartupTask : IBackgroundTask
  {
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    private BackgroundTaskDeferral _deferral;

    public async void Run(IBackgroundTaskInstance taskInstance)
    {
      Log.Info("-----------------------------------------------------------------------------");
      Log.Info($"Start server {typeof(StartupTask).GetTypeInfo().Assembly.GetName().Version}.");
      _deferral = taskInstance.GetDeferral();
      taskInstance.Canceled += TaskInstanceCanceled;
      await new Bootstrapper().RunAsync();
    }

    private void TaskInstanceCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
    {
      Log.Info($"Stop server {typeof(StartupTask).GetTypeInfo().Assembly.GetName().Version}.");
      _deferral.Complete();
    }
  }
}