namespace mauiApp;
using mauiApp.Pages;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        Routing.RegisterRoute(nameof(TodoPage), typeof(TodoPage));
        Routing.RegisterRoute(nameof(NotePages), typeof(NotePages));
        Routing.RegisterRoute("TodoEditPage", typeof(TodoEditPage));
        Routing.RegisterRoute("NotePage", typeof(NotePage));
        Routing.RegisterRoute("NoteEditPage", typeof(NoteEditPage));
    }
}
