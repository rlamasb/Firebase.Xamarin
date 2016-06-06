namespace Firebase.Xamarin.Query
{
	using System;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;

	using Firebase.Xamarin.Http;

	using Newtonsoft.Json;

	/// <summary>
	/// Firebase query which references the child of current node.
	/// </summary>
	public class ChildQuery : FirebaseQuery, IDisposable
    {
		private readonly string _path;
		private readonly string _secret;
        private HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildQuery"/> class.
        /// </summary>
        /// <param name="path"> The path to the child node. </param>
        /// <param name="parent"> The parent. </param>
        public ChildQuery(string path, FirebaseQuery parent, string secret = null)
            : base(parent)
        {
            _path = path;
			_secret = secret;

            if (!this._path.EndsWith("/", StringComparison.Ordinal))
            {
                this._path += "/";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildQuery"/> class.
        /// </summary>
        /// <param name="path"> The path to the child node. </param>
		public ChildQuery(string path, string secret = null)
            : this(path, null, secret)
        {
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }

        public async Task<FirebaseObject<T>> PostAsync<T>(T obj, bool generateKeyOffline = false)
        {
            // post generates a new key server-side, while put can be used with an already generated local key
            if (generateKeyOffline)
            {
                var key = FirebaseKeyGenerator.Next();
                await new ChildQuery(key, this).PutAsync(obj);

                return new FirebaseObject<T>(key, obj);
            }
            else
            {
                var c = this.GetClient();
                var data = await this.SendAsync(c, obj, HttpMethod.Post);
                var result = JsonConvert.DeserializeObject<PostResult>(data);

                return new FirebaseObject<T>(result.Name, obj);
            }
        }

        public async Task PutAsync<T>(T obj)
        {
            var c = this.GetClient();

            await this.SendAsync(c, obj, HttpMethod.Put);
        }

        public async Task DeleteAsync()
        {
            var c = this.GetClient();
            var url = this.BuildUrl();
            var result = await c.DeleteAsync(url);

            result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Build the url segment of this child.
        /// </summary>
        /// <param name="child"> The child of this child. </param>
        /// <returns> The <see cref="string"/>. </returns>
        protected override string BuildUrlSegment(FirebaseQuery child)
        {
			string path = _path;
            if (!(child is ChildQuery))
            {

				if (_path.EndsWith("/", StringComparison.Ordinal))
				{
					path = _path.Remove(_path.Length - 1);
				}

				 path += ".json";

				if (!string.IsNullOrEmpty(_secret)) path += "?auth=" + _secret;

                return path;
            }
			path = _path;

			if (!string.IsNullOrEmpty(_secret)) path += "?auth=" + _secret;



			return path;
        }

        private async Task<string> SendAsync<T>(HttpClient client, T obj, HttpMethod method)
        {
            var url = this.BuildUrl();
            var message = new HttpRequestMessage(method, url)
            {
				//Content = new StringContent(JsonConvert.SerializeObject(obj))
				Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"),

			};

            var result = await client.SendAsync(message);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        private HttpClient GetClient()
        {
            if (this.client == null)
            {
                this.client = new HttpClient();
            }

            return this.client;
        }
    }
}
