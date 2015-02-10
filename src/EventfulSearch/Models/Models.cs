using System;
using System.Collections.ObjectModel;

namespace EventfulSearch.Models
{
	public class SearchRequest
	{
		public string Address { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public float Radius { get; set; }
		public string Category { get; set; }

		public SearchRequest()
		{
		}
	}

	public class Event
	{
		public string EventMainImageUrl { get; set; }
		public string EventTitle { get; set; }
		public string VenueName { get; set; }
		public string ArtistsOrTeams { get; set; }
		public DateTime EventDate { get; set; }
	}
}