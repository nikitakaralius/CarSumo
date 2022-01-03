using System;

namespace Sources.Core.Tests
{
	[AttributeUsage(AttributeTargets.Field)]
	public class TestField : Attribute
	{
		public TestField(string id)
		{
			Id = id;
		}

		public string Id { get; }
	}
}