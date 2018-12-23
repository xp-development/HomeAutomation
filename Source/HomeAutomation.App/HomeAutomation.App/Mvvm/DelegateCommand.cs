using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeAutomation.App.Mvvm
{
  public class DelegateCommand<TExecuteArg, TCanExecuteArg> : ICommand
  {
    public DelegateCommand(Func<TExecuteArg, Task> executeMethod, Func<TCanExecuteArg, bool> canExecuteMethod = null)
    {
      _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
      _canExecuteMethod = canExecuteMethod;
    }

    public event EventHandler CanExecuteChanged;

    bool ICommand.CanExecute(object parameter)
    {
      return CanExecute((TCanExecuteArg)parameter);
    }

    async void ICommand.Execute(object parameter)
    {
      await Execute((TExecuteArg)parameter);
    }

    public Task Execute(TExecuteArg arg)
    {
      return _executeMethod(arg);
    }

    public bool CanExecute(TCanExecuteArg arg)
    {
      return _canExecuteMethod == null || _canExecuteMethod(arg);
    }

    private readonly Func<TCanExecuteArg, bool> _canExecuteMethod;
    private readonly Func<TExecuteArg, Task> _executeMethod;

    public void InvokeCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}