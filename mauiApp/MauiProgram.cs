using Backend.ViewModels;
using Microsoft.Extensions.Logging;
using mauiApp.Pages;
using Backend.Services;
using backend.Services;
using Microsoft.Extensions.Configuration;

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

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        builder.Logging.AddDebug();

        string baseUrl = configuration["ApiSettings:BaseUrl"];
        if (baseUrl == null) {
            throw new ArgumentNullException("ApiSettings:BaseUrl");
        }
        builder.Services.AddSingleton<HttpClient>(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
		builder.Services.AddSingleton<IApiTodoService, ApiTodoService>();
		builder.Services.AddSingleton<IApiNoteService, ApiNoteService>();

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
		builder.Services.AddSingleton<NotesViewModel>();

		builder.Services.AddTransient<NotePage>();
		builder.Services.AddTransient<NoteEditPage>();

        return builder.Build();
	}
}
