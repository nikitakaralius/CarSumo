using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.GameResources;
using DataModel.FileData;
using DataModel.GameData.GameSave;
using Menu.Resources;
using Services.Timers.Realtime;
using Sirenix.Utilities;
using Zenject;

namespace Infrastructure.Initialization
{
	public class ResourceTimersInitialization : IAsyncInitializable
	{
		private readonly DiContainer _container;
		private readonly TimersConfiguration _timersConfiguration;
		private readonly IResourcesConfiguration _fileConfiguration;
		private readonly IAsyncFileService _fileService;

		public ResourceTimersInitialization(DiContainer container,
											TimersConfiguration timersConfiguration,
											IResourcesConfiguration fileConfiguration,
											IAsyncFileService fileService)
		{
			_container = container;
			_timersConfiguration = timersConfiguration;
			_fileConfiguration = fileConfiguration;
			_fileService = fileService;
		}
		
		public async Task InitializeAsync()
		{
			SerializableResourceTimers serializableModel = await LoadModelFromFileAsync();

			ResourceTimers timers = serializableModel is null
				? EnsureCreated()
				: CreateFrom(serializableModel);
			
			BindToContainer(timers);
			BindSaves();
			Start(timers);
		}

		private static void Start(ResourceTimers timers) =>
			timers
				.All()
				.Select(x => x.Item1)
				.ForEach(timer => timer.Start());

		private ResourceTimers EnsureCreated() =>
			new ResourceTimers(new Dictionary<TimedResource, IRealtimeTimer>
			{
				{TimedResource.GameEnergy, new CyclicRealtimeTimer(DurationOf(TimedResource.GameEnergy))},
				{TimedResource.RewardedStoreEnergy, new CyclicRealtimeTimer(DurationOf(TimedResource.RewardedStoreEnergy))}
			});

		private ResourceTimers CreateFrom(SerializableResourceTimers model) =>
			new ResourceTimers(
				model.ResourceTimeLeft.ToDictionary(
					resourceTimeLeft => resourceTimeLeft.Key,
					resourceTimeLeft => new CyclicRealtimeTimer(
						DurationOf(resourceTimeLeft.Key),
						resourceTimeLeft.Value,
						model.LastSession) as IRealtimeTimer));

		private void BindToContainer(ResourceTimers timers) =>
			_container
				.BindInstance(timers)
				.AsSingle()
				.NonLazy();

		private void BindSaves()
		{
			_container
				.Bind<ResourceTimersSave>()
				.FromNew()
				.AsSingle()
				.NonLazy();

			_container.Resolve<ResourceTimersSave>();
		}
		
		private async Task<SerializableResourceTimers> LoadModelFromFileAsync()
		{
			string path = _fileConfiguration.ResourceTimersFilePath;
			return await _fileService.LoadAsync<SerializableResourceTimers>(path);
		}

		private TimeSpan DurationOf(TimedResource resource) => _timersConfiguration.RollbackDurationOf(resource);
	}
}