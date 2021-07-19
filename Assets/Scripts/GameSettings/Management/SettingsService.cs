using System.IO;
using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Structs;

namespace CarSumo.GameSettings.Management
{
    public class SettingsService
    {
        private const string FileName = "Settings.JSON";
        
        private readonly string _settingsRootDirectory;
        private readonly IFileService _fileService;

        public SettingsService(IFileService fileService, string settingsRootDirectory)
        {
            _fileService = fileService;
            _settingsRootDirectory = settingsRootDirectory;
        }
        
        public AllSettings Settings { get; private set; }

        private string FilePath => Path.Combine(_settingsRootDirectory, FileName);

        public void Init() => Load();

        public void Save()
        {
            _fileService.SaveAsync(Settings, FilePath);
        }

        public void Load()
        {
            Settings = _fileService.Load<AllSettings>(FilePath);
        }
    }
}