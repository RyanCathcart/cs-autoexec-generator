using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CreateConfigPage>();
        builder.Services.AddSingleton<CreateConfigViewModel>();

        builder.Services.AddSingleton<ConfigService>();
		builder.Services.AddSingleton(FolderPicker.Default);

        return builder.Build();
	}
}
