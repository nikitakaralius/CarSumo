using System.IO;
using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Management;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class SettingsServiceFactory : IFactory<SettingsService>
    {
        private readonly IFileService _fileService;

        public SettingsServiceFactory(IFileService fileService)
        {
            _fileService = fileService;
        }

        public SettingsService Create()
        {
            var settingsService = new SettingsService(_fileService, SettingsDirectory());
            settingsService.Init();
            
            return settingsService;
        }

        private string SettingsDirectory()
        {
            string assets = Application.isEditor ?
                Application.dataPath :
                Application.streamingAssetsPath;

            string path = Path.Combine(assets, "Settings");

            return path;
        }
    }
}