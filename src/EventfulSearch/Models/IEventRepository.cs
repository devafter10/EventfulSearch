using System;
using System.Collections.ObjectModel;
using EventfulSearch.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventfulSearch.Models
{
	public interface IEventRepository
	{
		List<Event> GetAllEvents(SearchRequest searchParam);

		Task<List<Event>> GetAllEventsAsync(SearchRequest searchParam);
	}
}