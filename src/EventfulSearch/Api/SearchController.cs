﻿using System;
using System.Collections;
using System.Collections.Generic;
using EventfulSearch.Models;
using EventfulSearch.Services;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventfulSearch.Controllers
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


		public IActionResult IsAddressValid(string address)
		{
			if (!ModelState.IsValid)
			{
				return new HttpStatusCodeResult(400);
			}

			var ret = _searchHelper.IsAddressValid(address);
			return new ObjectResult(ret);
		}


		[HttpGet]
		public async Task<IActionResult> GetEvents([Bind("Address", "StartDate", "EndDate", "Radius", "Category")] SearchRequest searchReq)
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