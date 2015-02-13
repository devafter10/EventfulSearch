using EventfulSearch.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventfulSearch.Controllers
{
    public class HomeController : Controller
    {
		[HttpGet]
		public IActionResult Index()
		{
			ViewData["Events"] = new SearchResponse();
			return View(new SearchRequest());
		}

		[HttpPost]
		public IActionResult GetRenderedEvents(SearchResponse response)
		{
			return PartialView("_Events", response);
		}
    }
}
