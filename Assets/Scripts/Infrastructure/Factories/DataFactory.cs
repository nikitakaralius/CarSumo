using System.IO;
using CarSumo.DataManagement.Core;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public abstract class DataFactory<T> : IFactory<T>
    {
        private const string DirectoryPath = "Data";

        protected DataFactory(IFileService fileService)
        {
            FileService = fileService;
            
            EnsureDirectoryCreated(SettingsDirectory);
        }

        protected IFileService FileService { get; }
        protected string SettingsDirectory => GetSettingsDirectory();

        public abstract T Create();
        
        private string GetSettingsDirectory()
        {
            string assets = Application.isEditor
                ? Application.dataPath
                : Application.persistentDataPath;


            string path = Path.Combine(assets, DirectoryPath);

            return path;
        }

        private void EnsureDirectoryCreated(string path)
        {
            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }
    }
}