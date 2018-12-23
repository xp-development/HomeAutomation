using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAutomation.App.Views.Shell
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationView : ContentPage
	{
	  public ListView NavigationListView => ListView;

    public NavigationView ()
		{
			InitializeComponent ();
		}
	}
}