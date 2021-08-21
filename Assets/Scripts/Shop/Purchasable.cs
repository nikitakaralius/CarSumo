using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
	public abstract class Purchasable : SerializedMonoBehaviour
	{
		[SerializeField] private IReadOnlyDictionary<ResourceId, int> _prices = new Dictionary<ResourceId, int>();
		[SerializeField] private IReadOnlyDictionary<ResourceId, TMP_Text> _priceTexts = new Dictionary<ResourceId, TMP_Text>();

		private PurchaseOperation _purchaseOperation = PurchaseOperation.Uninitialized;
		
		[Inject]
		private void Construct(IClientResourceOperations resourceOperations)
		{
			ResourceOperations = resourceOperations;
		}

		protected IClientResourceOperations ResourceOperations { get; private set; }

		private void OnValidate()
		{
			foreach (KeyValuePair<ResourceId, TMP_Text> pair in _priceTexts)
			{
				if (_prices.TryGetValue(pair.Key, out var price))
				{
					pair.Value.text = $"{price}";
				}
			}
		}

		public void TrySpend(ResourceId resource)
		{
			int price = _prices[resource];
			bool purchaseSuccessful = ResourceOperations.TrySpend(resource, price);

			if (purchaseSuccessful)
			{
				OnPurchaseCompleted();
				_purchaseOperation = new PurchaseOperation(resource, price);
			}
			else
			{
				OnPurchaseCanceled();
			}
		}

		protected abstract void OnPurchaseCompleted();

		protected abstract void OnPurchaseCanceled();

		protected void MakeRefund()
		{
			if (_purchaseOperation.Valid == false)
				return;
			
			ResourceOperations.Receive(_purchaseOperation.Resource, _purchaseOperation.Price);
		}
	}
}