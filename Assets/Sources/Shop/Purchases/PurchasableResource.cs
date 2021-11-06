using CarSumo.DataModel.GameResources;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableResource : Purchasable
	{
		[Header("Resources")]
		[SerializeField] private ResourceId _resource; 
		[SerializeField, Min(0)] private int _amount;
		[SerializeField, Required, SceneObjectsOnly] private TextMeshProUGUI _amountText;

		private IResourceStorage _resourceStorage;

		[Inject]
		private void Construct(IResourceStorage resourceStorage)
			=> _resourceStorage = resourceStorage;

		protected override void OnValidate()
		{
			base.OnValidate();
			_amountText.text = $"{_amount}";
		}

		protected override Bargain Validate()
		{
			int slotsAmount = _resourceStorage.GetResourceAmount(_resource).Value;
			int? slotsLimit = _resourceStorage.GetResourceLimit(_resource).Value;

			if (slotsLimit is null)
				return Bargain.Valid;

			return slotsAmount + _amount <= slotsLimit
				? Bargain.Valid
				: new Bargain($"The resource limit has been reached. Maximum number of resource is {slotsLimit}");
		}

		protected override void OnPurchaseCompleted()
		{
			ResourceOperations.Receive(_resource, _amount);
		}

		protected override void OnPurchaseCanceled(Bargain bargain)
		{
			Debug.Log(bargain.ExceptionMessage);
		}
	}
}