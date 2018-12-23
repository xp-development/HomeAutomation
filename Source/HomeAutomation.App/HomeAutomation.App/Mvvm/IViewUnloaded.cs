using System.Threading.Tasks;

namespace HomeAutomation.App.Mvvm
{
  public interface IViewUnloaded
  {
    Task UnloadedAsync();
  }
}