using EventfulSearch.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EventfulSearch.Models
{
	public class SearchHelper
	{
        private static readonly HashSet<string> ValidCategories = new HashSet<string>(new[] { "Music", "Sports", "Performing Arts" }, StringComparer.OrdinalIgnoreCase);
		private readonly IGoogleGeocodeService _geocodeSvc;

		public SearchHelper(IGoogleGeocodeService geocodeSvc)
		{
			_geocodeSvc = geocodeSvc;
        }

		public string CreateGeocode(GeocodeResponse geo)
		{
            if (geo == null || !string.Equals(geo.Status, "OK", StringComparison.OrdinalIgnoreCase) 
				|| string.IsNullOrEmpty(geo.Latitude) 
				|| string.IsNullOrEmpty(geo.Longitude))
			{
				return string.Empty;
			}

			var ret = string.Format("{0},{1}", geo.Latitude, geo.Longitude);
			return ret;
		}

		public bool IsValid(SearchRequest search)
		{
			if (search == null) return false;
			if (!string.IsNullOrEmpty(search.Category) && !ValidCategories.Contains(search.Category)) return false;

			search.Geocode = CreateGeocode(_geocodeSvc.GetGeocode(search.Address));
			if (string.IsNullOrEmpty(search.Geocode)) return false;
			
			return true;
		}
	}
}