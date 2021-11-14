using System;
using System.Linq;
using CarSumo.DataModel.GameResources;
using DataModel.DataPersistence;
using DataModel.GameData.Infrastructure;
using Menu.Resources;
using UniRx;

namespace DataModel.GameData.GameSave
{
	public class ResourceTimersSave
	{
		private readonly ResourceTimers _timers;
		private readonly IResourcesConfiguration _configuration;
		private readonly IAsyncFileService _fileService;

		public ResourceTimersSave(ResourceTimers timers,
									IResourcesConfiguration configuration,
									IAsyncFileService fileService,
									IApplicationEvents events)
		{
			_timers = timers;
			_configuration = configuration;
			_fileService = fileService;

			events
				.ObserveQuit()
				.Subscribe(_ => Save());

			events
				.ObservePaused()
				.Subscribe(pausedStatus =>
				{
					if (pausedStatus)
						Save();
				});
		}
		
		public void Save()
		{
			SerializableResourceTimers timers = ToSerializableResourceTimers(_timers);
			string path = _configuration.ResourceTimersFilePath;
			_fileService.SaveAsync(timers, path);
		}

		private SerializableResourceTimers ToSerializableResourceTimers(ResourceTimers timers) =>
			new SerializableResourceTimers
			{
				ResourceTimeLeft = timers
					.All()
					.ToDictionary(
						x => x.Item2,
						x => x.Item1.TimeLeft().Value),
				LastSession = DateTime.Now
			};
	}
}