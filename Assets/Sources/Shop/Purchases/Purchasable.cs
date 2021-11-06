using System.Collections.Generic;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using CarSumo.DataModel.GameResources;
using Shop.ExceptionMessaging;
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
		private IExceptionMessage _exceptionMessage;

		[Inject]
		private void Construct(IClientResourceOperations resourceOperations, ISoundEmitter soundEmitter, IExceptionMessage exceptionMessage)
		{
			ResourceOperations = resourceOperations;
			_soundEmitter = soundEmitter;
			_exceptionMessage = exceptionMessage;
		}

		protected IClientResourceOperations ResourceOperations { get; private set; }

		protected virtual void OnValidate()
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
			bool hasResources = ResourceOperations.TrySpend(resource, price);
			
			PurchaseOperation operation = new PurchaseOperation(resource, price);
			Bargain validatedBargain = Validate();
			
			if (hasResources && validatedBargain.IsValid)
			{
				OnPurchaseCompleted();
				OnPurchaseCompletedInternal();
				return;
			}

			if (HadResourcesButPurchaseIsNotValid())
				operation.Rollback(ResourceOperations);

			OnPurchaseCanceled(validatedBargain);
			OnPurchaseCanceledInternal(validatedBargain);

			bool HadResourcesButPurchaseIsNotValid() => hasResources;
		}

		protected abstract Bargain Validate();

		protected abstract void OnPurchaseCompleted();

		protected abstract void OnPurchaseCanceled(Bargain bargain);

		private void OnPurchaseCompletedInternal() => 
			_soundEmitter.Play(_purchasedCue);

		private void OnPurchaseCanceledInternal(Bargain bargain)
		{
			_soundEmitter.Play(_canceledCue);
			_exceptionMessage.Show(bargain.ExceptionMessage);
		}
	}
}