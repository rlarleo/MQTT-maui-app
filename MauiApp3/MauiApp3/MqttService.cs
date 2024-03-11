using Android.App;
using Android.Content;
using Android.OS;
using Plugin.LocalNotification;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet;
using Plugin.LocalNotification;


namespace MauiApp3;

[Service]
public class MqttService : Service
{
    private bool isRunning;
    private IMqttClient _mqttClient;
    private const int SERVICE_NOTIFICATION_ID = 1001;

    public override IBinder OnBind(Intent intent)
    {
        return null;
    }
    
    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {
        if (!isRunning)
        {
            isRunning = true;
            var topic = intent.GetStringExtra("topic");

            StartAsync(topic);
        }

        return StartCommandResult.Sticky;
    }
    
    private async Task StartAsync(string topic)
    {
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("192.168.1.4").Build();

        // Setup message handling before connecting so that queued messages
        // are also handled properly. When there is no event handler attached all
        // received messages get lost.
        _mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "test",
                Subtitle = "Hello",
                Description = "It's me",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1),
                    NotifyRepeatInterval = TimeSpan.FromSeconds(5),
                }
            };
            LocalNotificationCenter.Current.Show(request);
            
            return Task.CompletedTask;
        };
        var connectResult = await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        Console.WriteLine(connectResult.ResultCode);

        var mqttSubscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(topic)
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        Console.WriteLine("MQTT client subscribed to topic.");
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        isRunning = false;
        _mqttClient?.DisconnectAsync().GetAwaiter().GetResult();
    }
}