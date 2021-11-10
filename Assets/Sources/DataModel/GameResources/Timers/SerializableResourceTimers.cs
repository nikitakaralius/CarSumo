using System;
using System.Collections.Generic;

namespace Menu.Resources
{
	public class SerializableResourceTimers
	{
		public Dictionary<TimedResource, TimeSpan> ResourceTimeLeft { get; set; }
		public DateTime LastSession { get; set; }
	}
}