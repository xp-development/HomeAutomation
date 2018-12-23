using System;
using Grace.DependencyInjection;

namespace HomeAutomation.App.DependencyInjection
{
  public class ServiceLocator : IServiceLocator
  {
    private readonly DependencyInjectionContainer _container;

    public ServiceLocator(DependencyInjectionContainer container)
    {
      _container = container;
    }

    public T Get<T>()
    {
      return _container.Locate<T>();
    }

    public object Get(Type type)
    {
      return _container.Locate(type);
    }
  }
}