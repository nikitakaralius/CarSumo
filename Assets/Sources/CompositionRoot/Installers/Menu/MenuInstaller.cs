using Infrastructure.Installers.SubContainers;
using Sources.Cards;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
	public class MenuInstaller : MonoInstaller
	{
		[SerializeField] private VehicleCardsRepository _cards;
		
		public override void InstallBindings()
		{
			BindVehicleCardsRepository();
			ProcessSubContainers();
		}

		private void ProcessSubContainers()
		{
			InstantiationInstaller.Install(Container);
			TimersInstaller.Install(Container);
		}

		private void BindVehicleCardsRepository() =>
			Container
				.BindInterfacesTo<VehicleCardsRepository>()
				.AsSingle();
	}
}