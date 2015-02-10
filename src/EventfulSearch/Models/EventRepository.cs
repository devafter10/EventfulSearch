using System;
using System.Collections.ObjectModel;
using EventfulSearch.Services;

namespace EventfulSearch.Models
{
	public class EventRepository : IEventRepository
	{
		private readonly IEventfulService _eventfulService;

		public EventRepository(IEventfulService svc)
		{
			_eventfulService = svc;
		}

		public Collection<Event> GetAllEvents(Search searchParam)
		{
			//var eventCount = _eventfulService.GetEventCount(searchParam);
			return null;
		}

		public long GetAllEventCount(Search searchParam)
		{
			throw new NotImplementedException();
		}
	}
}