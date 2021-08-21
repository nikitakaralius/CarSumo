using DataModel.Vehicles;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableVehicle : Purchasable
	{
		[Header("Vehicle Preferences")]
		[SerializeField] private VehicleId _vehicle;
		
		private IVehicleStorageOperations _storageOperations;

		[Inject]
		private void Construct(IVehicleStorageOperations storageOperations)
		{
			_storageOperations = storageOperations;
		}

		protected override Purchase ValidatePurchase()
		{
			return Purchase.Valid;
		}

		protected override void OnPurchaseCompleted()
		{
			_storageOperations.Add(_vehicle);
		}

		protected override void OnPurchaseCanceled()
		{
			Debug.Log("Purchase canceled");	
		}
	}
}