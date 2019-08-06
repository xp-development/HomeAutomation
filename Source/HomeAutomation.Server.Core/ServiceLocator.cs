using System;
using Grace.DependencyInjection;

namespace HomeAutomation.Server.Core
{
  public class ServiceLocator : IServiceLocator
  {
    private readonly DependencyInjectionContainer _container;

    public ServiceLocator(DependencyInjectionContainer container)
    {
      _container = container;
    }

    public object Locate(Type type)
    {
      return _container.Locate(type);
    }
  }
}