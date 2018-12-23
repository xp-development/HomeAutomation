using System.ComponentModel;
using System.Threading.Tasks;
using HomeAutomation.App.Events;

namespace HomeAutomation.App.Mvvm
{
  public abstract class ViewModelBase : NotifyPropertyChangedBase, IViewLoaded, IViewUnloaded
  {
    protected readonly IEventAggregator EventAggregator;

    protected ViewModelBase(IEventAggregator eventAggregator)
    {
      EventAggregator = eventAggregator;
    }

    public override event PropertyChangedEventHandler PropertyChanged;

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