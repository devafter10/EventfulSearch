using System;
using System.Collections;
using System.Collections.Generic;
using EventfulSearch.Models;
using EventfulSearch.Services;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventfulSearch.Controllers
{
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
		public IActionResult Index()
		{
			return View(new SearchRequest());
		}

		[HttpPost]
		public IActionResult RequestSearch([Bind("Address", "StartDate", "EndDate", "Radius", "Category")] SearchRequest searchReq)
		{
			return null;
		}

        [HttpGet]
		public async Task<IActionResult> GetAllEvents([Bind("Address", "StartDate", "EndDate", "Radius", "Category")] SearchRequest searchReq)
		{
			if (!ModelState.IsValid || !_searchHelper.IsValid(searchReq))
			{
				return new HttpStatusCodeResult(400);
			}

			List<Event> allEvents = null;
			try
			{
				allEvents = await _eventRepository.GetAllEventsAsync(searchReq);
			}
			catch (Exception e)
			{
				//detail logging here
				// give generic error
				return new HttpStatusCodeResult(500);
			}

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