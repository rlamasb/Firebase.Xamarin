namespace Firebase.Xamarin.Auth
{
    /// <summary>
    /// The auth config. 
    /// </summary>
    public class FirebaseConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseConfig"/> class.
        /// </summary>
        /// <param name="apiKey"> The api key of your Firebase app. </param>
        public FirebaseConfig(string apiKey)
        {
            this.ApiKey = apiKey;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FirebaseConfig"/> class.
        /// </summary>
        /// <param name="apiKey">API key.</param>
        /// <param name="apiKeyForPushNotification">API key for push notification.</param>
        public FirebaseConfig(string apiKey, string apiKeyForPushNotification)
        {
            this.ApiKey = apiKey;
            this.ApiKeyForPushNotification = apiKeyForPushNotification;
        }

        /// <summary>
        /// Gets or sets the api key of your Firebase app. 
        /// </summary>
        public string ApiKey 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the API key for push notification.
        /// </summary>
        /// <value>The API key for push notification.</value>
        public string ApiKeyForPushNotification
        {
            get;
            set;
        }
    }
}
