using System.Collections.Generic;
using System.IO;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using CarSumo.DataModel.Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Settings
{
    [CreateAssetMenu(fileName = "ProjectConfiguration", menuName = "Configuration/ProjectConfiguration", order = 0)]
    public class ProjectConfiguration : ScriptableObject,
        IAudioConfiguration, IResourcesConfiguration, IAccountStorageConfiguration, IProjectConfiguration
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

        public string RootDirectoryName => Path.Combine(GetAssetsPath(), _rootDirectoryName);
        
        public string MusicVolumeParameter => _musicVolumeParameter;

        public string SfxVolumeParameter => _sfxVolumeParameter;

        public string AudioFilePath => GetFilePath(_audioFileName);

        public string ResourcesFilePath => GetFilePath(_resourcesFileName);

        public string AccountStorageFilePath => GetFilePath(_accountStorageFileName);

        public IEnumerable<string> GetDataFilePaths()
        {
            return new[] {AudioFilePath, ResourcesFilePath, AccountStorageFilePath};
        }

        private string GetFilePath(string fileName)
        {
            string assets = GetAssetsPath();

            string path = Path.Combine(assets, _rootDirectoryName, fileName) + Format;
            return path;
        }

        private static string GetAssetsPath()
        {
            return Application.isEditor
                ? Application.dataPath
                : Application.persistentDataPath;
        }
    }
}