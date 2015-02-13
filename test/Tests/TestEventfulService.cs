using System;
using Xunit;
using EventfulSearch.Services;
using EventfulSearch.Models;
using System.Linq;
using System.Diagnostics;

namespace Tests
{
    public class TestEventfulService
    {
		[Fact()]
	    public void TestService()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var search = new SearchRequest()
			{
				Address = "Vancouver",
				Category = "Music",
				StartDate = new DateTime(2015, 1, 1),
				EndDate = new DateTime(2015, 1, 1),
				Radius = 1f
			};
			
			var events = svc.GetEventsAsync(search);
		}

		[Fact()]
		public void TestRespTimeFormat()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var d = new DateTime(2015, 1, 24, 13, 49, 00);
			var str = d.ToString(svc.EventfulDateTimeResponseFormat);

			Assert.Equal("2015-01-24 13:49:00", str);
        }

    }
}