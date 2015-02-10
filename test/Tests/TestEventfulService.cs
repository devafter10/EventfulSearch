﻿using System;
using Xunit;
using EventfulSearch.Services;
using EventfulSearch.Models;
using System.Linq;
using System.Diagnostics;

namespace Tests
{
    public class TestEventfulService
    {
		[Fact]
	    public void TestService()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var search = new SearchRequest()
			{
				Address = "49.249660,-123.119340",
				Category = "Music",
				StartDate = new DateTime(2015, 1, 1),
				EndDate = new DateTime(2015, 1, 1),
				Radius = 1f
			};
			var events = svc.GetEvents(search);

			Console.WriteLine(string.Format("Event Count: {0}", events.Count));
			Assert.True(events.Any());
			Assert.Equal(16, events.Count);
		}

		[Fact]
		public void TestRespTimeFormat()
		{
			var svc = new EventfulService(new RestSharpProxy());
			var d = new DateTime(2015, 1, 24, 13, 49, 00);
			var str = d.ToString(svc.EVENTFUL_RESPONSE_FORMAT);

			Assert.Equal("2015-01-24 13:49:00", str);
        }

    }
}