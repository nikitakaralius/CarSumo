using System.IO;

namespace CarSumo.DataManagement.Core
{
    public abstract class DataService<TBase, TConcrete> where TConcrete : TBase
    {
        protected abstract string FileName { get; }

        private readonly string _rootDirectory;
        private readonly IFileService _fileService;

        protected DataService(IFileService fileService, string rootDirectory)
        {
            _fileService = fileService;
            _rootDirectory = rootDirectory;
        }

        public TBase StoredData { get; private set; }
        private string FilePath => Path.Combine(_rootDirectory, FileName);

        public void Init()
        {
            EnsureFileCreated();
            Load();
        }

        public void Load()
        {
            StoredData = _fileService.Load<TConcrete>(FilePath) ?? EnsureInitialized();
        }

        public void Save()
        {
            _fileService.SaveAsync(StoredData, FilePath);
        }

        protected abstract TBase EnsureInitialized();

        private void EnsureFileCreated()
        {
            if (File.Exists(FilePath))
                return;

            using var stream = new FileStream(FilePath, FileMode.CreateNew);
        }
    }
}