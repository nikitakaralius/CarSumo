using System.IO;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using CarSumo.DataModel.Settings;
using UnityEngine;

namespace Infrastructure.Settings
{
    [CreateAssetMenu(fileName = "ProjectConfiguration", menuName = "Configuration/ProjectConfiguration", order = 0)]
    public class ProjectConfiguration : ScriptableObject,
        IAudioConfiguration, IResourcesConfiguration, IAccountStorageConfiguration
    {
        private const string Format = ".JSON";
        
        [Header("Audio Configuration")]
        [SerializeField] private string _musicVolumeParameter;
        [SerializeField] private string _sfxVolumeParameter;
        
        [Header("Files Configuration")]
        [SerializeField] private string _rootDirectoryName;
        [SerializeField] private string _audioFileName;
        [SerializeField] private string _resourcesFileName;
        [SerializeField] private string _accountStorageFileName;

        public string MusicVolumeParameter => _musicVolumeParameter;

        public string SfxVolumeParameter => _sfxVolumeParameter;

        public string AudioFilePath => GetFilePath(_audioFileName);

        public string ResourcesFilePath => GetFilePath(_resourcesFileName);

        public string AccountStorageFilePath => GetFilePath(_accountStorageFileName);

        private void OnValidate()
        {
            EnsureDirectoryCreated(_rootDirectoryName);
            
            EnsureFileCreated(AudioFilePath);
            EnsureFileCreated(ResourcesFilePath);
            EnsureFileCreated(AccountStorageFilePath);
        }

        private string GetFilePath(string fileName)
        {
            string assets = Application.isEditor
                ? Application.dataPath
                : Application.persistentDataPath;

            string path = Path.Combine(assets, _rootDirectoryName, fileName) + Format;
            return path;
        }
        
        private void EnsureDirectoryCreated(string path)
        {
            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }

        private void EnsureFileCreated(string filePath)
        {
            if (File.Exists(filePath))
                return;

            using var stream = new FileStream(filePath, FileMode.CreateNew);
        }
    }
}