namespace mauiApp;
using mauiApp.Pages;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(TodoPage), typeof(TodoPage));
        Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
        Routing.RegisterRoute("TodoEditPage", typeof(TodoEditPage));
    }
}
