using RestSharp;
using System;
using System.Threading.Tasks;
using EventfulSearch.Models;
using System.Collections.Generic;

namespace EventfulSearch.Services
{
	public class GoogleGeocodeService : IGoogleGeocodeService
	{
		private readonly IRestProxy _proxy;

		public GoogleGeocodeService(IRestProxy proxy)
		{
			_proxy = proxy;

			_proxy.BaseUrl = "https://maps.googleapis.com/maps/api/geocode/xml";
			_proxy.ApiKey = new KeyValuePair<string, string>("key", "AIzaSyD66orgqVtAbC1brokj4jEIIt0M_MXe6vc");
        }

        private RestRequest Create(string address)
		{
			var request = new RestRequest(Method.GET);
			request.AddParameter("address", address);

			return request;
		}

		public GeocodeResponse GetGeocode(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return null;
			}

			var data = _proxy.Execute<GeocodeResponse>(Create(address));
			return data;
		}
	}
}