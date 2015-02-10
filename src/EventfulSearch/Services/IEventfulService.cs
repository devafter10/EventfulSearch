using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventfulSearch.Models;

namespace EventfulSearch.Services
{
	/*
	http://api.eventful.com/rest/events/search?app_key=XqcX3pSvZbQMH8cj&location=49.249660,-123.119340&date=2015010100-20150610100&category=Music&within=1&units=km
	*/
	public interface IEventfulService
	{
		Task<Collection<Event>> GetEvents();

		Task<long> GetEventCount();
	}
}