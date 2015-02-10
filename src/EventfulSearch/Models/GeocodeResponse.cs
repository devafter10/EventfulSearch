using System;
using System.Collections.ObjectModel;
using RestSharp.Deserializers;

namespace EventfulSearch.Models
{
	public class GeocodeResponse
    {
		[DeserializeAs(Name = "lng")]
		public string Longitude { get; set; }

		[DeserializeAs(Name = "lat")]
		public string Latitude { get; set; }

		public string Status { get; set; }
    
    }
}