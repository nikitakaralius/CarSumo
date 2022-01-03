using System;

namespace Sources.Core.Exceptions
{
	public class TestFieldNotFoundException : Exception
	{
		public TestFieldNotFoundException(string id)
		{
			Id = id;
		}

		public string Id { get; }
	}
}