using Shop;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
	public class ShopInstaller : MonoInstaller
	{
		[SerializeField] private ShopWindow _sceneShopWindow;
		
		public override void InstallBindings()
		{
			BindShopWindow();
		}

		private void BindShopWindow()
		{
			Container
				.BindInstance(_sceneShopWindow)
				.AsSingle();
		}
	}
}