using Plugin.LocalNotification;

namespace MauiApp3;

public partial class MainPage : ContentPage
{
    private readonly MqttService _mqttService;

    int count = 0;
    string loginName = "";

    public MainPage()
    {
        InitializeComponent();
        _mqttService = new MqttService();
    }

    private void OnLogin(object sender, EventArgs e)
    {
        MauiProgram.StopMqttService();

        loginName = NameEntry.Text;
        ViewBtn.Text = $"{loginName} 계정 로그인 중...";
        MauiProgram.StartMqttService(loginName);
    }    
    
    private void OnLogout(object sender, EventArgs e)
    {
        ViewBtn.Text = $"로그아웃 상태";
        
        MauiProgram.StopMqttService();
    }
}