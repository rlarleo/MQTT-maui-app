using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using MauiApp2;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace MauiApp2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureMauiHandlers((handlers) =>
            {
                StartBackgroundService(); // 백그라운드 서비스 호출
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void StartBackgroundService()
    {
        // NotificationBackgroundService 서비스를 시작
        var serviceIntent = new Android.Content.Intent(Android.App.Application.Context, typeof(NotificationBackgroundService));
        Android.App.Application.Context.StartService(serviceIntent);
    }
}