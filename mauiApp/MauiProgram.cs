using Backend.ViewModels;
using Microsoft.Extensions.Logging;
using mauiApp.Pages;
using Backend.Services;
using backend.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("mauiApp.appsettings.json");
        if (stream == null) {
            throw new InvalidOperationException("The resource stream 'mauiApp.appsettings.json' could not be found.");
        }

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();


        builder.Configuration.AddConfiguration(config);

        builder.Logging.AddDebug();

        Settings settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>();
        if (settings.BaseUrl == null) {
            throw new InvalidOperationException("The 'BaseUrl' setting is missing.");
        }
        builder.Services.AddSingleton<HttpClient>(sp => new HttpClient { BaseAddress = new Uri(settings.BaseUrl) });
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

    public class Settings {
        public string BaseUrl { get; set; }
    }
}
