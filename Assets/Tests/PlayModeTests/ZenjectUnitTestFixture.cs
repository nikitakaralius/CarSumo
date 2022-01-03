using NUnit.Framework;
using Zenject;

namespace Tests.PlayModeTests
{
	public abstract class ZenjectUnitTestFixture
	{
		protected DiContainer Container { get; private set; }

		[SetUp]
		public virtual void Setup()
		{
			Container = new DiContainer();
		}
	}
}