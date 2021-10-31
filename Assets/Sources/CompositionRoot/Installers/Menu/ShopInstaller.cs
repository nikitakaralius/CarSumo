using Shop;
using Shop.ExceptionMessaging;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
	public class ShopInstaller : MonoInstaller
	{
		[SerializeField] private ShopWindow _sceneShopWindow;
		[SerializeField] private ExceptionMessage _sceneExceptionMessage;
		
		
		public override void InstallBindings()
		{
			BindShopWindow();
			BindExceptionMessage();
		}

		private void BindShopWindow() =>
			Container
				.BindInstance(_sceneShopWindow)
				.AsSingle();

		private void BindExceptionMessage() =>
			Container
				.BindInterfacesAndSelfTo<ExceptionMessage>()
				.FromInstance(_sceneExceptionMessage)
				.AsSingle();
	}
}