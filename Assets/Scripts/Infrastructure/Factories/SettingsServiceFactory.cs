using System.IO;
using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Management;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class SettingsServiceFactory : IFactory<SettingsService>
    {
        private const string Directory = "Data";
        private readonly IFileService _fileService;

        public SettingsServiceFactory(IFileService fileService)
        {
            _fileService = fileService;
        }

        public SettingsService Create()
        {
            var settingsService = new SettingsService(_fileService, SettingsDirectory());
            
            return settingsService;
        }

        private string SettingsDirectory()
        {
            string assets = Application.isEditor ?
                Application.dataPath :
                Application.streamingAssetsPath;

            string path = Path.Combine(assets, Directory);

            return path;
        }
    }
}