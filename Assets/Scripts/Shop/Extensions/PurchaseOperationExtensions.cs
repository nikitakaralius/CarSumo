using CarSumo.DataModel.GameResources;

namespace Shop.Extensions
{
	public static class PurchaseOperationExtensions
	{
		public static void Rollback(this PurchaseOperation operation, IClientResourceOperations resourceOperations)
		{
			resourceOperations.Receive(operation.Resource, operation.Price);
		}
	}
}