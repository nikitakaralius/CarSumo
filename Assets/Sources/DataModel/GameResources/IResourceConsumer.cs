namespace CarSumo.DataModel.GameResources
{
	public interface IResourceConsumer
	{
		void Consume(ResourceId resource);
	}
}