using EventfulSearch.Models;
using System;
using Xunit;

namespace Tests
{
    public class TestExtensionMethods
    {
		[Fact]
		public void TestEventfulDateString() {
			var d = new DateTime(2016, 8, 12, 2, 3, 4, 5);
			var str = d.ToEventfulDateString();

			Assert.Equal("2016081200", str);

		}

    }
}