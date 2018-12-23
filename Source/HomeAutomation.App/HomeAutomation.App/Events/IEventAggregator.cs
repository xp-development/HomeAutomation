using System;
using System.Threading.Tasks;

namespace HomeAutomation.App.Events
{
  public interface IEventAggregator
  {
    void Subscribe<TEvent>(Func<TEvent, Task> action)
      where TEvent : IEvent;
    Task PublishAsync<TEvent>(TEvent @event)
      where TEvent : IEvent;
  }
}