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
		
		private IClientResourceOperations _resourceOperations;

		[Inject]
		private void Construct(IClientResourceOperations resourceOperations)
		{
			_resourceOperations = resourceOperations;
		}

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
			bool purchaseSuccessful = _resourceOperations.TrySpend(resource, price);
			
			if (purchaseSuccessful)
				OnPurchaseCompleted();
			else
				OnPurchaseCanceled();
		}

		protected abstract void OnPurchaseCompleted();

		protected abstract void OnPurchaseCanceled();
	}
}