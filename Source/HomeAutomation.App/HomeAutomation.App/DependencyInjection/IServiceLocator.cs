using System;

namespace HomeAutomation.App.DependencyInjection
{
  public interface IServiceLocator
  {
    T Get<T>();
    object Get(Type type);
  }
}