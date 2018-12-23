using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomation.App.Events
{
  public class EventAggregator : IEventAggregator
  {
    private readonly List<Delegate> _delegates = new List<Delegate>();

    public void Subscribe<TEvent>(Func<TEvent, Task> action)
      where TEvent : IEvent
    {
      _delegates.Add(action);
    }

    public async Task PublishAsync<TEvent>(TEvent @event)
      where TEvent : IEvent
    {
      foreach (var @delegate in _delegates)
      {
        if (@delegate is Func<TEvent, Task> action)
          await action(@event);
      }
    }
  }
}