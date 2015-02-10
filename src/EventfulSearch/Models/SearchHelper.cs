using System;
using System.Collections.Generic;
using System.Globalization;

namespace EventfulSearch.Models
{
	public class SearchHelper
	{
		private static readonly HashSet<string> validCategories = new HashSet<string>(new[] { "Music", "Sports", "Performing Arts" }, StringComparer.OrdinalIgnoreCase);

        public bool Validate(SearchRequest search)
		{
			if (search == null) return false;
			if (!validCategories.Contains(search.Category)) return false;
			if (string.IsNullOrEmpty(search.Address)) return false;
			if (search.Radius < 0 || search.Radius > 300) return false;
			if (search.StartDate == DateTime.MinValue) return false;
			if (search.EndDate == DateTime.MinValue) return false;

			return true;
		}

		public SearchRequest Validate(string address, string startDate, string endDate, float radius, string category)
		{
			var ret = new SearchRequest()
			{
				Address = address,
				Radius = radius,
				Category = category
			};

			DateTime dt = DateTime.MinValue;

			ret.StartDate = DateTime.MinValue;
			if (DateTime.TryParseExact(startDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
				ret.StartDate = dt;
			}

			ret.EndDate = DateTime.MinValue;
			if (DateTime.TryParseExact(endDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
			{
				ret.EndDate = dt;
			}

			ret = new SearchRequest()
			{
				Address = "49.249660,-123.119340",
				Category = "Music",
				StartDate = new DateTime(2015, 1, 1),
				EndDate = new DateTime(2015, 1, 1),
				Radius = 11f
			};

			return Validate(ret) ? ret : null;
		}

	
	}
}