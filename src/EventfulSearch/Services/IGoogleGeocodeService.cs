using EventfulSearch.Models;

namespace EventfulSearch.Services
{
	public interface IGoogleGeocodeService
	{
		GeocodeResponse GetGeocode(string address);
	}
}