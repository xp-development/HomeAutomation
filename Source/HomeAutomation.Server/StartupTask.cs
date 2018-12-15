using System;
using System.Reflection;
using Windows.ApplicationModel.Background;
using MetroLog;

namespace HomeAutomation.Server
{
  public sealed class StartupTask : IBackgroundTask
  {
    private BackgroundTaskDeferral _deferral;
    private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<StartupTask>();

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