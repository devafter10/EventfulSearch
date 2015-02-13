using EventfulSearch.Models;
using EventfulSearch.Services;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class TestSearchHelper
    {
		SearchHelper _helper;
		Mock<IGoogleGeocodeService> _mock = new Mock<IGoogleGeocodeService>();

		GeocodeResponse goodStatus = new GeocodeResponse()
		{
			Latitude = "1234",
			Longitude = "-1234",
			Status = "OK"
		};

		GeocodeResponse badStatus = new GeocodeResponse()
		{
			Latitude = "1234",
			Longitude = "-1234",
			Status = "Bad"
		};

		GeocodeResponse badLat = new GeocodeResponse()
		{
			Latitude = "",
			Longitude = "-1234",
			Status = "OK"
		};

		GeocodeResponse badLon = new GeocodeResponse()
		{
			Latitude = "1234",
			Longitude = "",
			Status = "OK"
		};

		public TestSearchHelper()
		{
		}

		[Fact]
		public void TestBadStatus()
		{
			_mock.Setup(s => s.GetGeocode("badStatus")).Returns(badStatus);
			_helper = new SearchHelper(_mock.Object);

			var ret = _helper.IsAddressValid("badStatus").Item1;
			Assert.False(ret);
		}

		[Fact]
		public void TestBadLat()
		{
			_mock.Setup(s => s.GetGeocode("badLat")).Returns(badLat);
			_helper = new SearchHelper(_mock.Object);

			var ret = _helper.IsAddressValid("badLat").Item1;
			Assert.False(ret);
		}

		[Fact]
		public void TestBadLon()
		{
			_mock.Setup(s => s.GetGeocode("badLon")).Returns(badLon);
			_helper = new SearchHelper(_mock.Object);

			var ret = _helper.IsAddressValid("badLon").Item1;
			Assert.False(ret);
		}

		[Fact]
		public void TestGoodGeocode()
		{
			_mock.Setup(s => s.GetGeocode("goodStatus")).Returns(goodStatus);
			_helper = new SearchHelper(_mock.Object);

			var ret = _helper.IsAddressValid("goodStatus");
			Assert.True(ret.Item1);
			Assert.Equal("1234,-1234", ret.Item2);
		}

		[Fact]
		public void TestIsValid()
		{
			Console.WriteLine("empty category (aka optional)");
			TestIsValidCore("", true);

			Console.WriteLine("mixed case");
			TestIsValidCore("mUSic", true);

			Console.WriteLine("mystery category");
			TestIsValidCore("does not exist", false);
		}

		public void TestIsValidCore(string category, bool expectTrue)
		{
			_mock.Setup(s => s.GetGeocode("goodStatus")).Returns(goodStatus);
			_helper = new SearchHelper(_mock.Object);

			var req = new SearchRequest()
			{
				Address = "goodStatus",
				Category = category
			};

			var ret = _helper.IsValid(req);

			if (expectTrue)
			{
				Assert.True(ret);
			} else
			{
				Assert.False(ret);
			}
		}
	}
}
