using Infrastructure.Installers.SubContainers;
using Menu.Cards;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
	public class MenuInstaller : MonoInstaller
	{
		[SerializeField] private CardStatsRepository _statsRepository;
		[SerializeField] private CardViewRepository _viewRepository;
		
		public override void InstallBindings()
		{
			ProcessSubContainers();
			BindStatsRepository();
			BindViewRepository();
		}

		private void ProcessSubContainers()
		{
			InstantiationInstaller.Install(Container);
			TimersInstaller.Install(Container);
		}

		private void BindStatsRepository()
		{
			Container
				.Bind<ICardStatsRepository>()
				.FromInstance(_statsRepository)
				.AsSingle();
		}
		
		private void BindViewRepository()
		{
			Container
				.Bind<ICardViewRepository>()
				.FromInstance(_viewRepository)
				.AsSingle();
		}
	}
}