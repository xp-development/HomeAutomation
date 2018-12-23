using System;

namespace HomeAutomation.App.Events
{
  public class NavigationEvent : IEvent
  {
    public NavigationEvent(Type viewType, object parameter = null)
    {
      ViewType = viewType;
      Parameter = parameter;
    }

    public Type ViewType { get; }
    public object Parameter { get; }
  }
}