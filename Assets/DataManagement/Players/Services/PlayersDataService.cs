using System.IO;
using CarSumo.DataManagement.Core;
using DataManagement.Players.Models;

namespace DataManagement.Players.Services
{
    public class PlayersDataService
    {
        private const string RepositoryPath = "PlayersRepository.JSON";
        
        private readonly string _playersRootDirectory;
        private readonly IFileService _fileService;

        public PlayersDataService(string playersRootDirectory, IFileService fileService)
        {
            _fileService = fileService;
            _playersRootDirectory = playersRootDirectory;
        }

        public IPlayersRepository Repository { get; private set; }

        private string FilePath => Path.Combine(_playersRootDirectory, RepositoryPath);

        public void Init()
        {
            Repository = _fileService.Load<PlayersRepository>(FilePath);
        }

        public void Save()
        {
            _fileService.Save(Repository, FilePath);
        }
    }
}