using Advertisement.Units.Rewarded;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class AdvertisementInstaller : Installer<AdvertisementInstaller>
	{
		public override void InstallBindings()
		{
			BindRewardedUnit();
		}

		private void BindRewardedUnit()
		{
			Container
				.BindInterfacesTo<IronSourceRewardedUnit>()
				.AsSingle();
		}
	}
}