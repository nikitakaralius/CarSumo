using CarSumo.DataModel.GameResources;
using UnityEngine;

namespace Shop
{
	public class ResourceSpendInvoker : MonoBehaviour
	{
		[SerializeField] private ResourceId _resource;
		[SerializeField] private Purchasable _purchasable;

		public void TrySpend()
		{
			_purchasable.TrySpend(_resource);
		}
	}
}