﻿using CommunityToolkit.Maui;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.Extensions.Logging;
using Microsoft.AppCenter.Analytics;

namespace MauiAppInsights
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // Added MauiCommunityToolkit
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews();
            // NOTE: Guidance to use AppCenter SDK only for crash reporting and analytics. This line is added to start AppCenter SDK.
            // For more information, visit: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/maui
            // Regular logging will be done using Application Insights SDK.
            AppCenter.Start("windowsdesktop=05ecf696-6e84-41cf-8fac-b82bb435d0cc;" +
                "android=634ca2d1-71bb-42d7-8971-f6cb61422ef5;" +
                "ios={Your iOS App secret here};" +
                "macos={Your macOS App secret here};",
                typeof(Analytics), typeof(Crashes));

            BuildApplicationInsights(builder);


            builder.Services.AddSingleton<ILogger>(sp =>
            {
                var factory = sp.GetRequiredService<ILoggerFactory>();
                return factory.CreateLogger("MauiAppInsights");
            });

            builder.Services.AddSingleton<MyService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void BuildApplicationInsights(MauiAppBuilder builder)
        {
            builder.Logging.AddApplicationInsights(configuration =>
            {
                configuration.TelemetryInitializers.Add(new ApplicationInitializer());
                // Replace with your Application Insights connection string 
                configuration.ConnectionString =
                "InstrumentationKey=78256835-5652-4d21-a15e-e898cdf30a82;IngestionEndpoint=https://southcentralus-3.in.applicationinsights.azure.com/;LiveEndpoint=https://southcentralus.livediagnostics.monitor.azure.com/";
            }, options =>
            {
                options.IncludeScopes = true;
                options.TrackExceptionsAsExceptionTelemetry = true;
                options.FlushOnDispose = true;
            });
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IMyService, MyService>();
            return mauiAppBuilder;
        }


        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            return mauiAppBuilder;
        }


        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<MainPage>();
            return mauiAppBuilder;
        }
    }
}
