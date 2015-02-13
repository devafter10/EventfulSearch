using System;
using Xunit;
using EventfulSearch.Services;
using EventfulSearch.Models;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Moq;
using RestSharp;

namespace Tests
{
    public class TestEventfulService
    {
		private class TestData
		{
			public int Total { get; set; }
			public int Times { get; set; }
			public string Message { get; set; }
		}

		[Fact()]
	    public void TestGetEvents()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var search = new SearchRequest()
			{
				Geocode = "45.6387281,-122.6614861",
				Address = "45.6387281,-122.6614861",
				Category = "Music",
				StartDate = new DateTime(2015, 2, 1),
				EndDate = new DateTime(2015, 2, 1),
				Radius = 1f,
			};
			
			var events = svc.GetEvents(search);
			Assert.Equal(5, events.Count);
		}

		[Fact()]
		public void TestGetTotal()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var search = new SearchRequest()
			{
				Geocode = "45.6387281,-122.6614861",
				Address = "45.6387281,-122.6614861",
                Category = "Music",
				StartDate = new DateTime(2015, 2, 1),
				EndDate = new DateTime(2015, 2, 1),
				Radius = 1f
			};

			var count = svc.GetEventCount(search);
			Assert.Equal(5, count);
		}

		[Fact()]
		public void TestNumberOfCallsMade()
		{
			var data = new TestData[] {
				new TestData{ Total = 0, Times = 0, Message = "zero" },
				new TestData{ Total = 2, Times = 1, Message = "once" },
				new TestData{ Total = 99, Times = 1, Message = "once" },
				new TestData{ Total = 100, Times = 1, Message = "exactly once" },
				new TestData{ Total = 101, Times = 2, Message = "twice" }
			};

			foreach (var d in data)
			{
				TestNumberOfCallsMadeCore(d);
			}
		}

		private void TestNumberOfCallsMadeCore(TestData d)
		{
			Console.WriteLine(d.Message);

			var req = new EventfulResponse() { TotalItems = d.Total };

			var mock = new Mock<IRestProxy>();
			// setup total count number
			mock.Setup(p => p.Execute<EventfulResponse>(It.IsAny<RestRequest>()))
				.Returns(req);

			var svc = new EventfulService(mock.Object);
			var tasks = svc.GetEventsAsync(new SearchRequest());

			mock.Verify(p => p.ExecuteAsync<EventfulResponse>(It.IsAny<RestRequest>()), Times.Exactly(d.Times));
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