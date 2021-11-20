using DataModel.GameData.Vehicles;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class VehicleAssetsProviderInstaller : MonoInstaller
	{
		[SerializeField] private VehicleAssetsSo _assets;
		
		public override void InstallBindings()
		{
			BindVehicleAssetsProvider();
		}

		private void BindVehicleAssetsProvider()
		{
			Container
				.Bind<IVehicleAssets>()
				.FromInstance(_assets)
				.AsSingle();
		}
	}
}