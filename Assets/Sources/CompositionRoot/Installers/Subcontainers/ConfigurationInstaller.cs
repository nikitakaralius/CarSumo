using CarSumo.DataModel.GameResources;
using Infrastructure.Settings;
using Menu.Resources;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class ConfigurationInstaller : MonoInstaller
	{
		[SerializeField] private ProjectConfiguration _debugConfiguration;
		[SerializeField] private ProjectConfiguration _releaseConfiguration;
		
		[SerializeField] private TimersConfiguration _timersConfiguration;
		[SerializeField] private GameResourceConsumption _resourceConsumption;
		
		public override void InstallBindings()
		{
			BindProjectConfiguration();
			BindTimersConfiguration();
			BindResourceConsumption();
		}
		
		private void BindProjectConfiguration() =>
			Container
				.BindInterfacesAndSelfTo<ProjectConfiguration>()
				.FromInstance(Application.isEditor
					? _debugConfiguration 
					: _releaseConfiguration)
				.AsSingle()
				.NonLazy();

		private void BindTimersConfiguration() =>
			Container
				.Bind<TimersConfiguration>()
				.FromInstance(_timersConfiguration)
				.AsSingle()
				.NonLazy();

		private void BindResourceConsumption()
		{
			Container.QueueForInject(_resourceConsumption);
			
			Container
				.Bind<IResourceConsumption>()
				.FromInstance(_resourceConsumption)
				.AsSingle();
		}
	}
}