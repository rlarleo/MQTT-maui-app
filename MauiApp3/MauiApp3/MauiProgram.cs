using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace MauiApp3;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var mqttService = new MqttService();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureMauiHandlers((handlers) =>
            {
                // StartBackgroundService(); // 백그라운드 서비스 호출
                // StartTestBackgroundService();
                // mqttService(); // MQTT 서비스 호출
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        return builder.Build();
    }

    public static void StartMqttService(string topic)
    {
        // MqttService 서비스를 시작
        var serviceIntent = new Android.Content.Intent(Android.App.Application.Context, typeof(MqttService));
        serviceIntent.PutExtra("topic", topic);

        Android.App.Application.Context.StartService(serviceIntent);
    }
    
    public static void StopMqttService()
    {
        var serviceIntent = new Android.Content.Intent(Android.App.Application.Context, typeof(MqttService));
        // 서비스 종료
        Android.App.Application.Context.StopService(serviceIntent);
    }
    
}