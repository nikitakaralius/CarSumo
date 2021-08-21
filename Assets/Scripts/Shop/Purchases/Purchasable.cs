using System.Collections.Generic;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using CarSumo.DataModel.GameResources;
using Shop.Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
	public abstract class Purchasable : SerializedMonoBehaviour
	{
		[Header("Resources")] 
		[SerializeField] private IReadOnlyDictionary<ResourceId, int> _prices = new Dictionary<ResourceId, int>();
		[SerializeField] private IReadOnlyDictionary<ResourceId, TMP_Text> _priceTexts = new Dictionary<ResourceId, TMP_Text>();

		[Header("Audio")]
		[SerializeField] private AudioCue _purchasedCue;
		[SerializeField] private AudioCue _canceledCue;

		private ISoundEmitter _soundEmitter;

		[Inject]
		private void Construct(IClientResourceOperations resourceOperations, ISoundEmitter soundEmitter)
		{
			ResourceOperations = resourceOperations;
			_soundEmitter = soundEmitter;
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
			PurchaseOperation operation = new PurchaseOperation(resource, price);

			Purchase validatedPurchase = ValidatePurchase();
			if (purchaseSuccessful && validatedPurchase.IsValid)
			{
				OnPurchaseCompleted();
				OnPurchaseCompletedInternal();
				return;
			}

			if (purchaseSuccessful)
				operation.Rollback(ResourceOperations);

			OnPurchaseCanceled(validatedPurchase);
			OnPurchaseCanceledInternal();
		}

		protected abstract Purchase ValidatePurchase();

		protected abstract void OnPurchaseCompleted();

		protected abstract void OnPurchaseCanceled(Purchase purchase);

		private void OnPurchaseCompletedInternal()
			=> _soundEmitter.Play(_purchasedCue);

		private void OnPurchaseCanceledInternal()
			=> _soundEmitter.Play(_canceledCue);
	}
}