using Backend.ViewModels;
using Microsoft.Extensions.Logging;
using mauiApp.Pages;
using Backend.Services;

namespace mauiApp;

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Logging.AddDebug();

        // Todos
        builder.Services.AddSingleton<IApiService, MockApiService>();

        builder.Services.AddSingleton<TodoPage>();
		builder.Services.AddSingleton<TodoViewModel>();

        builder.Services.AddTransient<TodoEditPage>();

        // Notes
        builder.Services.AddSingleton<NotePages>();
		builder.Services.AddSingleton<NodesViewModel>();

		builder.Services.AddTransient<NotePage>();

        return builder.Build();
	}
}
