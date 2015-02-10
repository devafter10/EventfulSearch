using System;
using System.Collections.ObjectModel;

namespace EventfulSearch.Models
{
	public class Search
	{
		public string Address { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public float Radius { get; set; }

		public int Category { get; set; }

		public Collection<Event> Events { get; set; }

		public Search()
		{
			Address = "123 fake";
			StartDate = DateTime.UtcNow;
			EndDate = DateTime.UtcNow - TimeSpan.FromDays(1);
			Radius = 9.9f;
			Category = 2;

			Events = new Collection<Event>();
			Events.Add(new Event() {});
			Events.Add(new Event() { });
			Events.Add(new Event() { });
			
		}
	}

	public class Event
	{
	}
}