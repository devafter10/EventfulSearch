using System;
using Xunit;

namespace Tests
{
    public class Class1
    {
        public Class1()
        {
        }

		[Fact]
		public void TestOnePlusOne()
		{
			int result = 1 + 1;
			Assert.Equal(2, result);
		}

		[Fact]
		public void TestFailure()
		{
			int result = 1 + 1;
			Assert.Equal(6, result);
		}

	}
}
