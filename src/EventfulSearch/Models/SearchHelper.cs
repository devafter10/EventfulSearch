using EventfulSearch.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EventfulSearch.Models
{
	public class SearchHelper
	{
		private const string OK = "OK";
        private static readonly HashSet<string> ValidCategories = new HashSet<string>(new[] { "Music", "Sports", "Performing Arts" }, StringComparer.OrdinalIgnoreCase);
		private readonly IGoogleGeocodeService _geocodeSvc;

		public SearchHelper(IGoogleGeocodeService geocodeSvc)
		{
			_geocodeSvc = geocodeSvc;
        }

		public string CreateGeocode(GeocodeResponse geo)
		{
            if (geo == null || !string.Equals(geo.Status, OK, StringComparison.OrdinalIgnoreCase) 
				|| string.IsNullOrEmpty(geo.Latitude) 
				|| string.IsNullOrEmpty(geo.Longitude))
			{
				return string.Empty;
			}

			var ret = string.Format("{0},{1}", geo.Latitude, geo.Longitude);
			return ret;
		}

		public Tuple<bool, string> IsAddressValid(string address)
		{
			var geocode = CreateGeocode(_geocodeSvc.GetGeocode(address));
			var validGeocode = !string.IsNullOrEmpty(geocode);

			return new Tuple<bool, string>(validGeocode, geocode);
		}

		public bool IsValid(SearchRequest search)
		{
			if (search == null) return false;
			if (!string.IsNullOrEmpty(search.Category) && !ValidCategories.Contains(search.Category)) return false;

			var tuple = IsAddressValid(search.Address);
			if (!tuple.Item1) return false;

			search.Geocode = tuple.Item2;
			return true;
		}
	}
}