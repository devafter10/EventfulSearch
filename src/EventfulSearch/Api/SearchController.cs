using System;
using System.Collections;
using System.Collections.Generic;
using EventfulSearch.Models;
using EventfulSearch.Services;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventfulSearch.Api
{
	[Route("api/[controller]")]
	public class SearchController : Controller
	{
		private readonly SearchHelper _searchHelper;
		private readonly IEventRepository _eventRepository;

		public SearchController(IEventRepository repository, SearchHelper helper)
		{
			_eventRepository = repository;
			_searchHelper = helper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEvents(string address, string startDate, string endDate, float radius, string category)
		{
			var search = _searchHelper.Validate(address, startDate, endDate, radius, category);
            if (search == null)
			{
				return new ObjectResult(new Event() { EventTitle = "Invalid search request" });
            }

			var allEvents = await _eventRepository.GetAllEventsAsync(search);
			if (!allEvents.Any())
			{
				allEvents.Add(new Event() { EventTitle = "No match found" });
			}

			return new ObjectResult(allEvents);
		}
	}
}