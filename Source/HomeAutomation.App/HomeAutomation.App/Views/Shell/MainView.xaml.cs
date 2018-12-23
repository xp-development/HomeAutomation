using System;
using System.Threading.Tasks;
using HomeAutomation.App.DependencyInjection;
using HomeAutomation.App.Events;
using HomeAutomation.App.Mvvm;
using Xamarin.Forms;

namespace HomeAutomation.App.Views.Shell
{
  public partial class MainView
  {
    private readonly IServiceLocator _serviceLocator;

    public MainView(IServiceLocator serviceLocator, IEventAggregator eventAggregator)
    {
      _serviceLocator = serviceLocator;

      InitializeComponent();

      NavigationView.NavigationListView.ItemSelected += ListViewOnItemSelected;

      eventAggregator.Subscribe(new Func<MessageBoxEvent, Task>(OnMessageBoxEvent));
      eventAggregator.Subscribe(new Func<NavigationEvent, Task>(OnNavigationEvent));
    }

    private async Task OnMessageBoxEvent(MessageBoxEvent obj)
    {
      var result = await DisplayAlert(obj.Title, obj.Message, obj.Accept, obj.Cancel);
      obj.Result = result;
    }

    private Task OnNavigationEvent(NavigationEvent obj)
    {
      return Navigate(obj.ViewType, obj.Parameter);
    }

    private async void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      if (!(e.SelectedItem is NavigationItem item))
        return;

      await Navigate(item.TargetType);
    }

    private async Task Navigate(Type viewType, object parameter = null)
    {
      if ((Detail as NavigationPage)?.CurrentPage?.BindingContext is IViewUnloaded viewUnloaded)
        await viewUnloaded.UnloadedAsync();

      Detail = new NavigationPage((Page)_serviceLocator.Get(viewType));
      if (((NavigationPage)Detail)?.CurrentPage?.BindingContext is IViewLoaded viewLoaded)
        await viewLoaded.LoadedAsync(parameter);

      NavigationView.NavigationListView.SelectedItem = null;
      IsPresented = false;
    }
  }
}