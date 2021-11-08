using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.GameResources;
using DataModel.FileData;
using Menu.Resources;
using Services.Timers.Realtime;
using Zenject;

namespace Infrastructure.Initialization
{
	public class ResourceTimersInitialization : IAsyncInitializable
	{
		private readonly DiContainer _container;
		private readonly IResourcesConfiguration _configuration;
		private readonly IAsyncFileService _fileService;

		public ResourceTimersInitialization(DiContainer container, IResourcesConfiguration configuration, IAsyncFileService fileService)
		{
			_container = container;
			_configuration = configuration;
			_fileService = fileService;
		}
		public async Task InitializeAsync()
		{
			SerializableResourceTimers serializableModel = await LoadModelFromFileAsync();

			ResourceTimers timers = serializableModel is null
				? EnsureCreated()
				: CreateFrom(serializableModel);
			
			BindToContainer(timers);
		}

		private ResourceTimers EnsureCreated() =>
			new ResourceTimers(new Dictionary<TimedResource, IRealtimeTimer>()
			{
				{TimedResource.GameEnergy, new OfflineRealtimeTimer()},
				{TimedResource.RewardedStoreEnergy, new OfflineRealtimeTimer()}
			});

		private ResourceTimers CreateFrom(SerializableResourceTimers model) =>
			new ResourceTimers(
				model.ResourceTimeLeft.ToDictionary(
					resourceTimeLeft => resourceTimeLeft.Key,
					resourceTimeLeft => new OfflineRealtimeTimer(resourceTimeLeft.Value) as IRealtimeTimer));

		private void BindToContainer(ResourceTimers timers) =>
			_container
				.BindInstance(timers)
				.AsSingle()
				.NonLazy();

		private async Task<SerializableResourceTimers> LoadModelFromFileAsync()
		{
			string path = _configuration.ResourceTimersFilePath;
			return await _fileService.LoadAsync<SerializableResourceTimers>(path);
		}
	}
}