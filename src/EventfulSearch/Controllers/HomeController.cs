using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace EventfulSearch.Controllers
{
    public class HomeController : Controller
    {
		public SelectList ValidCategoryItems { get; set; }

		public HomeController()
		{
			ValidCategoryItems = new SelectList(new[]
				{
					new SelectListItem() { Text = "Music", Selected = true },
					new SelectListItem() { Text = "Sports"},
					new SelectListItem() { Text = "Performing Arts"}
				});
		}

		public IActionResult Index()
        {
			ViewBag.ValidCategoryItems = ValidCategoryItems;
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}