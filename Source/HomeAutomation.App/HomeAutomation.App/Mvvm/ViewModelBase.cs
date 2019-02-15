using System.ComponentModel;
using System.Threading.Tasks;

namespace HomeAutomation.App.Mvvm
{
  public abstract class ViewModelBase : NotifyPropertyChangedBase, IViewLoaded, IViewUnloaded
  {
    public Task LoadedAsync(object parameter)
    {
      return OnLoadedAsync(parameter);
    }

    public async Task UnloadedAsync()
    {
      await OnUnloadedAsync();
    }

    protected virtual Task OnLoadedAsync(object parameter)
    {
      return Task.CompletedTask;
    }

    protected virtual Task OnUnloadedAsync()
    {
      return Task.CompletedTask;
    }
  }
}