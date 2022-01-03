using DataModel.Vehicles;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableVehicle : Purchasable
	{
		[Header("Vehicle Preferences")]
		[SerializeField] private Vehicle _vehicle;
		
		private IVehicleStorageOperations _storageOperations;

		[Inject]
		private void Construct(IVehicleStorageOperations storageOperations)
		{
			_storageOperations = storageOperations;
		}

		protected override Bargain Validate()
		{
			return Bargain.Valid;
		}

		protected override void OnPurchaseCompleted()
		{
			_storageOperations.Add(_vehicle);
		}

		protected override void OnPurchaseCanceled(Bargain bargain)
		{
			
		}
	}
}