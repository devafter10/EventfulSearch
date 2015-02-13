using System;
using Xunit;
using EventfulSearch.Services;

namespace Tests
{
    public class TestGoogleGeocodeService
    {
	    public TestGoogleGeocodeService()
	    {
		    
	    }

	    [Fact()]
	    public void TestService()
	    {
		    var svc = new GoogleGeocodeService(new RestSharpProxy());

		    var resp = svc.GetGeocode("123 fakestreet");
		    Assert.Equal("38.7062039", resp.Latitude);
		    Assert.Equal("-78.5065001", resp.Longitude);
		    Assert.Equal("OK", resp.Status);
	    }
    }
}