﻿using System;
using System.Collections.ObjectModel;

namespace EventfulSearch.Models
{
    public class EventfulResponse
    {
		public int TotalItems { get; set; }
		public int PageSize { get; set; }

		public int PageCount { get; set; }

		public int PageNumber { get; set; }

		public Collection<EventfulEvent> Events { get; set; }
	}

	public class EventfulEvent
	{
		public string Title { get; set; }
		public string Url { get; set; }

		public string Description { get; set; }

		public string StartTime { get; set; }

		public string StopTime { get; set; }

		public string Price { get; set; }

		public string VenueName { get; set; }

		public string VenueUrl { get; set; }

		public string VenueType { get; set; }

		public string VenueAddress { get; set; }

		public float Latitude { get; set; }

		public float Longitude { get; set; }
	}
}