namespace mauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(TodoPage), typeof(TodoPage));
		Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
    }
}
