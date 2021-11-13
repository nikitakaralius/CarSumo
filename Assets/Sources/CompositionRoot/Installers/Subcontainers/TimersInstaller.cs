using Menu.Resources;
using Sirenix.Utilities;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class TimersInstaller : Installer<TimersInstaller>
	{
		[Inject] private readonly ResourceTimers _timers;

		public override void InstallBindings()
		{
			BindTimerTickable();
		}
		
		private void BindTimerTickable()
		{
			_timers
				.All()
				.ForEach(tuple =>
					Container
						.Bind<ITickable>()
						.FromInstance(tuple.Item1)
						.AsSingle()
						.NonLazy());

		}
	}
}