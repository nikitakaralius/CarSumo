using System;
using CarSumo.DataModel.GameResources;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableAccountSlot : Purchasable
	{
		[Range(0, 100)] 
		[SerializeField] private int _amount;
		[SerializeField] private TMP_Text _countText;

		private IResourceStorage _resourceStorage;

		[Inject]
		private void Construct(IResourceStorage resourceStorage)
			=> _resourceStorage = resourceStorage;

		private void OnValidate() => _countText.text = $"{_amount}";

		protected override void OnPurchaseCompleted()
		{
			int slotsAmount = _resourceStorage.GetResourceAmount(ResourceId.AccountSlots).Value;
			int? slotsLimit = _resourceStorage.GetResourceLimit(ResourceId.AccountSlots).Value;

			if (slotsLimit is null)
			{
				MakeRefund();
				throw new InvalidOperationException("Slots limit must be specified");
			}

			if (slotsAmount + _amount <= slotsLimit)
			{
				ResourceOperations.Receive(ResourceId.AccountSlots, _amount);
			}
			else
			{
				MakeRefund();
				throw new InvalidOperationException("Reached slots limit");
			}
		}

		protected override void OnPurchaseCanceled()
			=> Debug.Log("Purchase canceled");
	}
}