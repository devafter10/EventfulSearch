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

		// string address, string startDate, string endDate, float radius, string category
		[HttpGet]
		public async Task<IActionResult> GetAllEvents([Bind("Address", "StartDate", "EndDate", "Radius", "Category")] SearchRequest search)
		{
			if (!ModelState.IsValid)
			{
				return new HttpStatusCodeResult(400);
			}

			var allEvents = await _eventRepository.GetAllEventsAsync(search);

			// no events found
			if (!allEvents.Any())
			{
				return HttpNotFound();
			}

			var resp = new SearchResponse()
			{
				Events = allEvents,
				TotalNumberOfEvents = allEvents.Count,
				Duration = "0"
			};

			return new ObjectResult(resp);
		}
	}
}