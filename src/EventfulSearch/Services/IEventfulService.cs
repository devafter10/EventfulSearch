using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventfulSearch.Models;
using System.Collections.Generic;

namespace EventfulSearch.Services
{
	/*
	http://api.eventful.com/rest/events/search?app_key=XqcX3pSvZbQMH8cj&location=49.249660,-123.119340&date=2015010100-20150610100&category=Music&within=1&units=km
	*/
	public interface IEventfulService
	{
		Task<List<EventfulEvent>> GetEventsAsync(SearchRequest search, GeocodeModel geoCode);
		List<EventfulEvent> GetEvents(SearchRequest search, GeocodeModel geoCode);
		int GetEventCount(SearchRequest search, GeocodeModel geoCode);

		string EventfulDateTimeResponseFormat { get; }
    }
}