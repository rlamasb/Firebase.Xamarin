namespace Firebase.Xamarin
{
	using Query;

	/// <summary>
	/// Firebase client which acts as an entry point to the online database.
	/// </summary>
	public class FirebaseClient<T>
	{
		private readonly string _baseUrl;
		private readonly string _secret;

		/// <summary>
		/// Initializes a new instance of the <see cref="FirebaseClient"/> class.
		/// </summary>
		/// <param name="baseUrl"> The base url. </param>
		public FirebaseClient(string baseUrl, string secret)
		{
			_secret = secret;
			_baseUrl = baseUrl;
		}

		/// <summary>
		/// Queries for a child of the data root.
		/// </summary>
		/// <param name="resourceName"> Name of the child. </param>
		/// <returns> <see cref="ChildQuery"/>. </returns>
		public ChildQuery Child(string resourceName)
		{
			string path = _baseUrl + resourceName;

			return new ChildQuery(path, secret:_secret);
		}
		/// <summary>
		/// Subscribes to data async.
		/// </summary>
		/// <returns>A token from which you can subscribe to events.</returns>
		/// <param name="path">Path.</param>
		public StreamToken<T> GetStreamToken(string path) => new StreamToken<T>(Child(path).AsObservable<T>());
	}
}