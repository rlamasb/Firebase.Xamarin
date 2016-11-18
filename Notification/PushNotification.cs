using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Xamarin.Auth;

namespace Firebase.Xamarin.Notification
{
  public class PushNotification
  {
    private const string NotificationUrl = "https://fcm.googleapis.com/fcm/send";

    private readonly FirebaseConfig authConfig;

    public PushNotification(FirebaseConfig authConfig)
    {
      this.authConfig = authConfig;
    }

    public async Task Send(string to, string title, string message)
    {
      using (HttpClient client = new HttpClient())
      {
        var postContent = $"{{\"to\":\"{to}\",\"notification\":{{ \"title\":\"{title}\",\"text\":\"{message}\" }}}}";

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", $"={this.authConfig.ApiKeyForPushNotification}");
        var response = await client.PostAsync(NotificationUrl, new StringContent(postContent, Encoding.UTF8, "application/json")).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
      }
    }
  }
}
