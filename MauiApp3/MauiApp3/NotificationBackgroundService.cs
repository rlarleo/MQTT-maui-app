using Android.App;
using Android.Content;
using Android.OS;
using Plugin.LocalNotification;
using System;

namespace MauiApp3
{
    [Service]
    public class NotificationBackgroundService : Service
    {
        private bool isRunning;
        private NotificationManager notificationManager;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (!isRunning)
            {
                isRunning = true;
                StartSendingNotifications();
            }

            return StartCommandResult.Sticky;
        }

        private async void StartSendingNotifications()
        {
            notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            while (isRunning)
            {
             
                var request = new NotificationRequest
                {
                    NotificationId = 1337,
                    Title = "Subscribe to my channel",
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

                // 알림 발송 간격
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isRunning = false;
        }
    }
}