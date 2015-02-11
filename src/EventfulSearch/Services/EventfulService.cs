using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventfulSearch.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace EventfulSearch.Services
{
	public class EventfulService : IEventfulService
	{
		private readonly IRestProxy _proxy;

		private const int LARGE_NUMBER = 100;
		public string EventfulDateTimeResponseFormat
		{
			get
			{
				return "yyyy-MM-dd HH:mm:ss";
			}
		}

		public EventfulService(IRestProxy proxy)
		{
			_proxy = proxy;

			_proxy.BaseUrl = "http://api.eventful.com/rest/events/search";
			_proxy.ApiKey = new KeyValuePair<string, string>("app_key", "XqcX3pSvZbQMH8cj");
        }

		private RestRequest Create(SearchRequest search, int pageNumber)
		{
			var request = new RestRequest(Method.GET);

			request.AddParameter("location", string.IsNullOrEmpty(search.Geocode) ? search.Geocode : search.Address);
			request.AddParameter("date", string.Format("{0}-{1}", search.StartDate.ToEventfulDateString(), search.EndDate.ToEventfulDateString()));
			request.AddParameter("category", search.Category);
			request.AddParameter("within", search.Radius);
			request.AddParameter("units", "km");
			request.AddParameter("include", "price");
			request.AddParameter("page_number", pageNumber);
			request.AddParameter("page_size", LARGE_NUMBER);
			// request.DateFormat = EVENTFUL_RESPONSE_FORMAT;
		
			return request;
		}

		public int GetEventCount(SearchRequest search)
		{
			var request = Create(search, 1);
			request.AddParameter("count_only", true);

			// execute the request
			var data = _proxy.Execute<EventfulResponse>(request);

			return data.TotalItems;
		}

		public List<EventfulEvent> GetEvents(SearchRequest search)
		{
			var task = GetEventsAsync(search);

			// block until Result is available. ConfigureAsync(false) in used in lower levels so shouldn't deadlock
			return task.Result;
		}

		public async Task<List<EventfulEvent>> GetEventsAsync(SearchRequest search)
		{
			EventfulResponse[] collectionOfResponse = null;
			int totalEvent = 0;

            try
			{
				totalEvent = GetEventCount(search);
				var numOfCallsNeeded = (int)Math.Ceiling((double)totalEvent / LARGE_NUMBER);

				var tasks = new List<Task<EventfulResponse>>(numOfCallsNeeded);
				for (int i = 1; i <= numOfCallsNeeded; i++)
				{
					var request = Create(search, i);
					tasks.Add(_proxy.ExecuteAsync<EventfulResponse>(request));
				}

				collectionOfResponse = await Task.WhenAll(tasks).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				throw new ApplicationException("Error interfacing with Eventful", e);
			}

			var ret = new List<EventfulEvent>(totalEvent);
			foreach (var resp in collectionOfResponse)
			{
				ret.AddRange(resp.Events);
			}

            return ret;
		}
	}
}