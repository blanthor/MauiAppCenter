using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;

namespace MauiAppCenter3;

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

        AppCenter.Start("windowsdesktop=05ecf696-6e84-41cf-8fac-b82bb435d0cc;" +
			"android=634ca2d1-71bb-42d7-8971-f6cb61422ef5;" +
			"ios={Your iOS App secret here};" +
			"macos={Your macOS App secret here};",
			typeof(Analytics), typeof(Crashes));


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
