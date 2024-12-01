using Backend.ViewModels;
using Microsoft.Extensions.Logging;
using mauiApp.Pages;
using Backend.Services;
using backend.Services;

namespace mauiApp;

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Logging.AddDebug();

        builder.Services.AddSingleton<HttpClient>(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5110") });
		builder.Services.AddSingleton<IApiTodoService, ApiTodoService>();
		builder.Services.AddSingleton<IApiNoteService, ApiNodeService>();

        // pages
        // Main
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        // Todos
        builder.Services.AddSingleton<TodoPage>();
		builder.Services.AddSingleton<TodoViewModel>();

        builder.Services.AddTransient<TodoEditPage>();

        // Notes
        builder.Services.AddSingleton<NotePages>();
		builder.Services.AddSingleton<NodesViewModel>();

		builder.Services.AddTransient<NotePage>();
		builder.Services.AddTransient<NoteEditPage>();

        return builder.Build();
	}
}
