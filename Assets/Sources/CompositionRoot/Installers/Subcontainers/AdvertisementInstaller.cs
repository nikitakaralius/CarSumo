using Advertisement.Units.Interstitial;
using Advertisement.Units.Rewarded;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
	public class AdvertisementInstaller : Installer<AdvertisementInstaller>
	{
		public override void InstallBindings()
		{
			BindRewardedUnit();
			BindInterstitialUnit();
		}

		private void BindRewardedUnit()
		{
			Container
				.BindInterfacesTo<IronSourceRewardedUnit>()
				.AsSingle();
		}

		private void BindInterstitialUnit()
		{
			Container
				.BindInterfacesTo<IronSourceInterstitialUnit>()
				.AsSingle();
		}
	}
}