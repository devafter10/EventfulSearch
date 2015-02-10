using System;

namespace EventfulSearch.Models
{
	public class SearchHelper
	{
		public Search Build(string address, DateTime startDate, DateTime endDate, float radius, string category)
		{
			var ret = new Search();

			return ret;
		}

		public bool Validate(Search Search)
		{
			throw new NotImplementedException();
		}
	}
}