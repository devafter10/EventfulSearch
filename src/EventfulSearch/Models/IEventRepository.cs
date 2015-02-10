using System;
using System.Collections.ObjectModel;
using EventfulSearch.Controllers;

namespace EventfulSearch.Models
{
	public interface IEventRepository
	{
		Collection<Event> GetAllEvents(Search searchParam);

		long GetAllEventCount(Search searchParam);
	}
}