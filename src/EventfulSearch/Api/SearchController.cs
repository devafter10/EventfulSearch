using System;
using System.Collections;
using System.Collections.Generic;
using EventfulSearch.Models;
using EventfulSearch.Services;
using Microsoft.AspNet.Mvc;

namespace EventfulSearch.Api
{
	[Route("api/[controller]")]
	public class SearchController : Controller
	{
		//private readonly IGoogleGeocodeService _ggcSvc;
		private readonly SearchHelper _searchHelper;
		private readonly IEventRepository _eventRepository;

		public SearchController(IEventRepository repository, SearchHelper helper)
		{
			_eventRepository = repository;
			_searchHelper = helper;
		}

		[HttpGet]
		public IEnumerable<Event> GetAllEvents([FromQuery] Search search)
		{
			if (!_searchHelper.Validate(search))
			{
				return null;
			}

			return null;
		}
	}


}