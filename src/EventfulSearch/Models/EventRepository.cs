using System;
using System.Collections.ObjectModel;
using EventfulSearch.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

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
		
		/// <summary>
		/// This is needed as there's a bug in RestSharp on parsing DateTime. RestSharp would not
		/// convert to DateTime even though I specify exactly the same DateTimeFormat
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		private Event ConvertEvent(EventfulEvent arg)
		{
			DateTime dt = DateTime.MinValue;
			if (!DateTime.TryParseExact(arg.StartTime, _eventfulSvc.EventfulDateTimeResponseFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
			{
				dt = DateTime.MinValue;
			}

			var ret = new Event()
			{
				ArtistsOrTeams = arg.Performers?.FirstOrDefault()?.Name ?? string.Empty,
				EventDate = dt,
				EventMainImageUrl = arg.Image.Url ?? string.Empty,
				EventTitle = arg.Title,
				VenueName = arg.VenueName
			};

			return ret;
		}

		public async Task<List<Event>> GetAllEventsAsync(SearchRequest searchParam)
		{
			var allEventfulEvents = _eventfulSvc.GetEventsAsync(searchParam);

			var allEvents = (await allEventfulEvents).Select(ConvertEvent);
			return allEvents.ToList();
		}
	}
}