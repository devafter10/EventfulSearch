using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventfulSearch.Services
{
	public interface IRestProxy
	{
		string BaseUrl { get; set; }
		KeyValuePair<string, string> ApiKey { get; set; }

		Task<T> ExecuteAsync<T>(IRestRequest request) where T : new();
        T Execute<T>(IRestRequest request) where T : new();
    }
}