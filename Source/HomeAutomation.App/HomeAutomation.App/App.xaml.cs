using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HomeAutomation.App
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      var bootstrapper = new Bootstrapper();
      MainPage = bootstrapper.Run();
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
