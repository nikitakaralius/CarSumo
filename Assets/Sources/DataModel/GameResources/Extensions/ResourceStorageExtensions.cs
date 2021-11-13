namespace CarSumo.DataModel.GameResources.Extensions
{
	public static class ResourceStorageExtensions
	{
		public static bool OnItsLimit(this IResourceStorage storage, ResourceId resource)
		{
			int amount = storage.GetResourceAmount(resource).Value;
			int? limit = storage.GetResourceLimit(resource).Value;

			return amount == limit;
		}
	}
}