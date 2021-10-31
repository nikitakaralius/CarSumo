using System;
using Advertisement.Units.Rewarded;
using CarSumo.DataModel.GameResources;
using Shop.ExceptionMessaging;
using UnityEngine;
using Zenject;

namespace Shop.RewardedItems
{
	public class Rewarded : MonoBehaviour
	{
		[SerializeField] private RewardedPlacement _placement;
		[SerializeField] private ResourceId _resource;

		private IRewardedUnit _unit;
		private IClientResourceOperations _operations;
		private IExceptionMessage _exceptionMessage;
		private IResourceStorage _resourceStorage;

		[Inject]
		private void Construct(IRewardedUnit unit, IClientResourceOperations operations, IResourceStorage storage, IExceptionMessage message)
		{
			_unit = unit;
			_operations = operations;
			_exceptionMessage = message;
			_resourceStorage = storage;
		}

		public void Show() =>
			_unit.Show(_placement, rewardAmount =>
			{
				Bargain bargain = Validate(rewardAmount);
				
				if (bargain.IsValid == false)
				{
					_exceptionMessage.Show(bargain.ExceptionMessage);
					return;
				}
				
				_operations.Receive(_resource, rewardAmount);
			});

		private Bargain Validate(int amount)
		{
			int slotsAmount = _resourceStorage.GetResourceAmount(ResourceId.AccountSlots).Value;
			int? slotsLimit = _resourceStorage.GetResourceLimit(ResourceId.AccountSlots).Value;

			if (slotsLimit is null)
				throw new InvalidOperationException("Slots limit must be specified");

			return slotsAmount + amount <= slotsLimit
				? Bargain.Valid
				: new Bargain($"The slot limit has been reached. Maximum number of slots is {slotsLimit}");
		}
	}
}