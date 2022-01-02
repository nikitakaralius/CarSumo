using System;

namespace Sources.Core.Tests
{
	[AttributeUsage(AttributeTargets.Field)]
	public class FindInTestAttribute : Attribute
	{
		public FindInTestAttribute(string id)
		{
			Id = id;
		}

		public string Id { get; }
	}
}