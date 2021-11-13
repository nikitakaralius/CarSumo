using CarSumo.DataModel.GameResources;
using Infrastructure.Settings;
using Menu.Resources;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class ConfigurationInstaller : MonoInstaller
	{
		[SerializeField] private ProjectConfiguration _projectConfiguration;
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
				.FromInstance(_projectConfiguration)
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