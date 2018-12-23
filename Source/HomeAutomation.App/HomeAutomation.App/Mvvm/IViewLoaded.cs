using System.Threading.Tasks;

namespace HomeAutomation.App.Mvvm
{
  public interface IViewLoaded
  {
    Task LoadedAsync(object parameter = null);
  }
}