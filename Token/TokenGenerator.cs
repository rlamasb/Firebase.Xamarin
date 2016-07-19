using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Firebase.Xamarin.Token
{
	public class TokenGenerator
	{
		private static int TOKEN_VERSION = 0;
		private string _firebaseSecret;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="firebaseSecret">The Firebase Secret for your firebase (can be found by entering your Firebase URL into a web browser, and clicking the "Auth" pane).</param>
		public TokenGenerator(string firebaseSecret)
		{
			_firebaseSecret = firebaseSecret;
		}

		/// <summary>
		/// Creates an authentication token containing arbitrary auth data.
		/// </summary>
		/// <param name="data">Arbitrary data that will be passed to the Firebase Rules API, once a client authenticates.  Must be able to be serialized to JSON with <see cref="System.Web.Script.Serialization.JavaScriptSerializer"/>.</param>
		/// <returns>The auth token.</returns>
		public string CreateToken(Dictionary<string, object> data)
		{
			return CreateToken(data, new TokenOptions());
		}

		/// <summary>
		/// Creates an authentication token containing arbitrary auth data and the specified options.
		/// </summary>
		/// <param name="data">Arbitrary data that will be passed to the Firebase Rules API, once a client authenticates.  Must be able to be serialized to JSON with <see cref="System.Web.Script.Serialization.JavaScriptSerializer"/>.</param>
		/// <param name="options">A set of custom options for the token.</param>
		/// <returns>The auth token.</returns>
		public string CreateToken(Dictionary<string, object> data, TokenOptions options)
		{
			var dataEmpty = (data == null || data.Count == 0);
			if (dataEmpty && (options == null || (!options.admin && !options.debug)))
			{
				throw new Exception("data is empty and no options are set.  This token will have no effect on Firebase.");
			}

			var claims = new Dictionary<string, object>();
			claims["v"] = TOKEN_VERSION;
			claims["iat"] = secondsSinceEpoch(DateTime.Now);

			var isAdminToken = (options != null && options.admin);
			validateToken(data, isAdminToken);

			if (!dataEmpty)
			{
				claims["d"] = data;
			}

			// Handle options.
			if (options != null)
			{
				if (options.expires.HasValue)
					claims["exp"] = secondsSinceEpoch(options.expires.Value);
				if (options.notBefore.HasValue)
					claims["nbf"] = secondsSinceEpoch(options.notBefore.Value);
				if (options.admin)
					claims["admin"] = true;
				if (options.debug)
					claims["debug"] = true;
			}

			var token = computeToken(claims);
			if (token.Length > 1024)
			{
				throw new Exception("Generated token is too long. The token cannot be longer than 1024 bytes.");
			}
			return token;
		}

		private string computeToken(Dictionary<string, object> claims)
		{
			return JWT.JsonWebToken.Encode(claims, this._firebaseSecret, JWT.JwtHashAlgorithm.HS256);
		}

		private static long secondsSinceEpoch(DateTime dt)
		{
			TimeSpan t = dt.ToUniversalTime() - new DateTime(1970, 1, 1);
			return (long)t.TotalSeconds;
		}

		private static void validateToken(Dictionary<string, object> data, Boolean isAdminToken)
		{
			var containsUid = (data != null && data.ContainsKey("uid"));
			if ((!containsUid && !isAdminToken) || (containsUid && !(data["uid"] is string)))
			{
				throw new Exception("Data payload must contain a \"uid\" key that must not be a string.");
			}
			else if (containsUid && data["uid"].ToString().Length > 256)
			{
				throw new Exception("Data payload must contain a \"uid\" key that must not be longer than 256 characters.");
			}
		}
	}
}

