using System.Collections.Generic;
using System.Linq;
using AI.Structures;

namespace AI.Extensions
{
	public static class EnumerableExtensions
	{
		public static VehiclePair Closest(this IEnumerable<VehiclePair> pairs)
		{
			VehiclePair closest = pairs.FirstOrDefault();

			foreach (VehiclePair pair in pairs)
			{
				if (pair.SqrDistance < closest.SqrDistance)
					closest = pair;
			}

			return closest;
		}
	}
}