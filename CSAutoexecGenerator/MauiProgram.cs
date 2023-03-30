using CSAutoexecGenerator.Services;
using CSAutoexecGenerator.ViewModels;
using CSAutoexecGenerator.Views;
using Microsoft.Extensions.Logging;

namespace CSAutoexecGenerator;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<ConfigService>();
        builder.Services.AddTransient<MainViewModel>();

        return builder.Build();
	}
}
