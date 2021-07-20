using System.Collections.Generic;
using CarSumo.DataManagement.Core;
using DataManagement.Players.Models;

namespace DataManagement.Players.Services
{
    public class PlayersDataService : DataService<IPlayersRepository, PlayersRepository>
    {
        public PlayersDataService(IFileService fileService, string rootDirectory) : base(fileService, rootDirectory)
        {
        }

        protected override string FileName => "PlayersRepository.JSON";
        
        protected override IPlayersRepository EnsureInitialized()
        {
            return new PlayersRepository()
            {
                Items = new List<Player>(),
                SelectedPlayerIndex = -1
            };
        }
    }
}