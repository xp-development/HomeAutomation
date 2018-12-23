namespace HomeAutomation.App.DependencyInjection
{
  public static class StaticServiceLocator
  {
    public static IServiceLocator Current { get; private set; }

    internal static void SetCurrent(IServiceLocator serviceLocator)
    {
      Current = serviceLocator;
    }
  }
}