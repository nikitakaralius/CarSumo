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

		private PurchaseOperation(ResourceId resource, int price, bool valid)
		{
			Resource = resource;
			Price = price;
			Valid = valid;
		}

		public static PurchaseOperation Uninitialized = new PurchaseOperation(ResourceId.Energy, -1, false);
	}
}