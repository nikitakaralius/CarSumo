using System.IO;
using Infrastructure.Settings;

namespace Infrastructure.Initialization
{
    public class DataFilesInitialization
    {
        private readonly IProjectConfiguration _configuration;

        public DataFilesInitialization(IProjectConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize()
        {
            EnsureDirectoryCreated(_configuration.RootDirectoryName);
            
            foreach (string dataFilePath in _configuration.GetDataFilePaths())
            {
                EnsureFileCreated(dataFilePath);
            }
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