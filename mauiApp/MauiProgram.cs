﻿using Backend.ViewModels;
using Microsoft.Extensions.Logging;

namespace mauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Logging.AddDebug();

		builder.Services.AddSingleton<TodoPage>();
		builder.Services.AddSingleton<TodoViewModel>();

		builder.Services.AddSingleton<NotePage>();

		return builder.Build();
	}
}
