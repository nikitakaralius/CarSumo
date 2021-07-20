using System.IO;
using CarSumo.DataManagement.Core;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public abstract class DataFactory<T> : IFactory<T>
    {
        private const string Directory = "Data";

        protected DataFactory(IFileService fileService)
        {
            FileService = fileService;
        }

        protected IFileService FileService { get; }
        protected string SettingsDirectory => GetSettingsDirectory();

        public abstract T Create();
        
        private string GetSettingsDirectory()
        {
            string assets = Application.isEditor ?
                Application.dataPath :
                Application.streamingAssetsPath;

            string path = Path.Combine(assets, Directory);

            return path;
        }
    }
}