using CarSumo.DataModel.GameResources;

namespace Shop
{
	public readonly struct PurchaseOperation
	{
		public ResourceId Resource { get; }
		public int Price { get; }
		public bool Valid { get; }

		public PurchaseOperation(ResourceId resource, int price)
		{
			Resource = resource;
			Price = price;
			Valid = true;
		}
	}
}