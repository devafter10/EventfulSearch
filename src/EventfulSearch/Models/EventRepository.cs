using System;
using System.Collections.ObjectModel;
using EventfulSearch.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EventfulSearch.Models
{
	public class EventRepository : IEventRepository
	{
		private readonly IEventfulService _eventfulSvc;
		private readonly IGoogleGeocodeService _geocodeSvc;

		public EventRepository(IEventfulService eventfulSvc, IGoogleGeocodeService geocodeSvc)
		{
			_eventfulSvc = eventfulSvc;
			_geocodeSvc = geocodeSvc;
		}

		private Event ConvertEvent(EventfulEvent arg)
		{
			var ret = new Event()
			{
				ArtistsOrTeams = arg.Title,
				EventDate = DateTime.UtcNow,
				EventMainImageUrl = arg.Price,
				EventTitle = arg.Title,
				VenueName = arg.VenueName
			};

			return ret;
		}

		public List<Event> GetAllEvents(SearchRequest searchParam)
		{
			var allEventfulEvents = _eventfulSvc.GetEvents(searchParam);

			var allEvents = allEventfulEvents.Select(ConvertEvent);
			return allEvents.ToList();
		}

		public async Task<List<Event>> GetAllEventsAsync(SearchRequest searchParam)
		{
			var allEventfulEvents = _eventfulSvc.GetEventsAsync(searchParam);

			var allEvents = (await allEventfulEvents).Select(ConvertEvent);
			return allEvents.ToList();
		}
	}
}