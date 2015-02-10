using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace EventfulSearch.Models
{
	public static class ExtensionMethods
	{
		public static IList<T> AddRangeIfValid<T>(this List<T> list, ICollection<T> toBeAdded)
		{
			if (toBeAdded == null)
			{
				toBeAdded = new T[0];
			}

			list.AddRange(toBeAdded.Where(t => t != null));

			return list;
		}

		public static string ToEventfulDateString(this DateTime dt)
		{
			return dt.Date.ToString("yyyyMMdd00");
		}
	}
}