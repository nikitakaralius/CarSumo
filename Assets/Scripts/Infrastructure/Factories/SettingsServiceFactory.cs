using System.IO;
using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Services;
using UnityEngine;

namespace CarSumo.Infrastructure.Factories
{
    public class SettingsServiceFactory : DataFactory<SettingsService>
    {
        public SettingsServiceFactory(IFileService fileService) : base(fileService) { }

        public override SettingsService Create()
        {
            return new SettingsService(FileService, SettingsDirectory);;
        }
    }
}