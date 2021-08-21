using System;
using CarSumo.DataModel.GameResources;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableAccountSlot : Purchasable
	{
		[Header("Accounts Preferences")]
		[Range(0, 100)] 
		[SerializeField] private int _amount;
		[SerializeField] private TMP_Text _countText;

		private IResourceStorage _resourceStorage;

		[Inject]
		private void Construct(IResourceStorage resourceStorage)
			=> _resourceStorage = resourceStorage;

		private void OnValidate() => _countText.text = $"{_amount}";

		protected override Purchase ValidatePurchase()
		{
			int slotsAmount = _resourceStorage.GetResourceAmount(ResourceId.AccountSlots).Value;
			int? slotsLimit = _resourceStorage.GetResourceLimit(ResourceId.AccountSlots).Value;

			if (slotsLimit is null)
				throw new InvalidOperationException("Slots limit must be specified");

			return slotsAmount + _amount <= slotsLimit
				? Purchase.Valid
				: new Purchase($"The slot limit has been reached. Maximum number of slots is {slotsLimit}");
		}

		protected override void OnPurchaseCompleted()
		{
			ResourceOperations.Receive(ResourceId.AccountSlots, _amount);
		}

		protected override void OnPurchaseCanceled(Purchase purchase)
		{
			Debug.Log(purchase.ExceptionMessage);
		}
	}
}