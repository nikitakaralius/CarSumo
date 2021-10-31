using UnityEngine;
using Zenject;

namespace Shop
{
	public class ShopWindowOpenInvoker : MonoBehaviour
	{
		[SerializeField] private ShopTab _tab;
		
		private ShopWindow _shopWindow;

		[Inject]
		private void Construct(ShopWindow shopWindow)
		{
			_shopWindow = shopWindow;
		}

		public void OpenShop()
		{
			_shopWindow.OpenOnTab(_tab);
		}
	}
}