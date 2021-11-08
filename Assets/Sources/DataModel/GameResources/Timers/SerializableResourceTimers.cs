using System;
using System.Collections.Generic;

namespace Menu.Resources
{
	public class SerializableResourceTimers
	{
		public Dictionary<TimedResource, DateTime> ResourceTimeLeft { get; set; }
	}
}