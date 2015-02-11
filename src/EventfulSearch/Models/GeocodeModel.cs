using System;

namespace EventfulSearch.Models
{
    public class GeocodeModel
    {
		public string Geocode { get; set; }

		public GeocodeModel(GeocodeResponse geo)
		{
			Geocode = string.Empty;

			if (geo == null || string.IsNullOrEmpty(geo.Latitude) || string.IsNullOrEmpty(geo.Longitude))
			{
				return;
			}

			if (string.Equals(geo.Status,"OK", StringComparison.OrdinalIgnoreCase))
			{
				Geocode = string.Format("{0},{1}", geo.Latitude, geo.Longitude);
			}
		}
    }
}