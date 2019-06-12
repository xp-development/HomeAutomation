using System;

namespace HomeAutomation.Server.Core
{
  public interface IServiceLocator
  {
    object Locate(Type type);
  }
}