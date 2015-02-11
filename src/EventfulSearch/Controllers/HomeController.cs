using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using EventfulSearch.Models;

namespace EventfulSearch.Controllers
{
    public class HomeController : Controller
    {
		private readonly SearchHelper _helper;

		public IEnumerable<SelectListItem> ValidCategoryItems { get; set; }

		public HomeController(SearchHelper helper)
		{
			_helper = helper;

			ValidCategoryItems = new SelectList(new[]
				{
					new SelectListItem { Value = "Music", Text = "Music" },
					new SelectListItem { Value = "Sports", Text = "Sports" },
					new SelectListItem { Value = "Performing Arts", Text = "Performing Arts" }
				});
		}

		public IActionResult Index()
        {
			ViewBag.ValidCategoryItems = ValidCategoryItems;
            return View(new SearchRequest());
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}