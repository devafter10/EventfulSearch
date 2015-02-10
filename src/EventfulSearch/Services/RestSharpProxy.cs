using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventfulSearch.Services
{
	public class RestSharpProxy : IRestProxy
	{
		public string BaseUrl { get; set; }
		public KeyValuePair<string, string> ApiKey { get; set; }

		public async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
		{
			if (request == null)
			{
				return default(T);
			}

			request.AddParameter(ApiKey.Key, ApiKey.Value);
			var client = new RestClient(BaseUrl);
			var responseTask = client.ExecuteTaskAsync<T>(request);
			var ret = (await responseTask.ConfigureAwait(false)).Data;

			return ret;
		}

		public T Execute<T>(IRestRequest request) where T : new()
		{
			if (request == null)
			{
				return default(T);
			}

			request.AddParameter(ApiKey.Key, ApiKey.Value);
			var client = new RestClient(BaseUrl);
			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var exception = new ApplicationException(message, response.ErrorException);
				throw exception;
			}

			return response.Data;
		}
	}
}