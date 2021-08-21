using DataModel.Vehicles;
using UnityEngine;
using Zenject;

namespace Shop
{
	public class PurchasableVehicle : Purchasable
	{
		[SerializeField] private VehicleId _vehicle;
		
		private IVehicleStorageOperations _storageOperations;

		[Inject]
		private void Construct(IVehicleStorageOperations storageOperations)
		{
			_storageOperations = storageOperations;
		}
	
		protected override void OnPurchaseCompleted()
		{
			_storageOperations.AddVehicle(_vehicle);
		}

		protected override void OnPurchaseCanceled()
		{
			Debug.Log("Purchase canceled");	
		}
	}
}