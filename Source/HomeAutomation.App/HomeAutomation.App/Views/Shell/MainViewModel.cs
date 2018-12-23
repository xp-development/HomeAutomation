using HomeAutomation.App.Events;
using HomeAutomation.App.Mvvm;

namespace HomeAutomation.App.Views.Shell
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
    {
    }
  }
}