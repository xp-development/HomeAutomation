using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAutomation.App.Mvvm
{
  public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
  {
    public virtual event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected void InvokePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}