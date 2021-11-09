using System;
using System.Linq;
using CarSumo.DataModel.GameResources;
using DataModel.FileData;
using Menu.Resources;

namespace DataModel.GameData.GameSave
{
	public class ResourceTimersSave : IDisposable
	{
		private readonly ResourceTimers _timers;
		private readonly IResourcesConfiguration _configuration;
		private readonly IAsyncFileService _fileService;

		public ResourceTimersSave(ResourceTimers timers, IResourcesConfiguration configuration, IAsyncFileService fileService)
		{
			_timers = timers;
			_configuration = configuration;
			_fileService = fileService;
		}

		public void Dispose() => Save();

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