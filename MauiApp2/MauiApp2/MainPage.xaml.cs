using Plugin.LocalNotification;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

		LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
	}

	private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
	{
		if (e.IsDismissed)
		{

		}
		else if (e.IsTapped)
		{

		}
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
		
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
		
	}
}


