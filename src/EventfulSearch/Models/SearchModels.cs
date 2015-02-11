using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventfulSearch.Models
{
	public class SearchRequest
	{
		[Required]
		[MinLength(1)]
		public string Address { get; set; }

		[Display(Name = "Start Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
		[Required]
		public DateTime StartDate { get; set; }

		[Display(Name = "End Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
		[Required]
		public DateTime EndDate { get; set; }

		[Display(Name = "Radius (km)")]
		[Range(0, 300)]
		public float? Radius { get; set; }

		public string Category { get; set; }

		public string Geocode { get; set; }

		public SearchRequest()
		{
			Address = string.Empty;
			StartDate = DateTime.Today;
			EndDate = DateTime.Today + TimeSpan.FromDays(1);
			Radius = 1f;
			Category = "Music";
		}
	}

	public class SearchResponse
	{
		public int TotalNumberOfEvents { get; set; }
		public string Duration { get; set; }
		public List<Event> Events { get; set; }
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