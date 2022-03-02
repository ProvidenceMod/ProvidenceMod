using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providence
{
	public static class ProvidenceExtensions
	{
		public static void AddIfTrue(this List<string> list, bool boolean, string item)
		{
			if (boolean)
				list.Add(item);
		}
		public static void AddIfFalse(this List<string> list, bool boolean, string item)
		{
			if (!boolean)
				list.Add(item);
		}
	}
}
