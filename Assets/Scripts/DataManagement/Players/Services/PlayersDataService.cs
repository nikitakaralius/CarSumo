using System.Collections.Generic;
using CarSumo.DataManagement.Core;
using DataManagement.Players.Models;
using DataManagement.Resources;

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
                Items = new List<Player>()
                {
                    new Player()
                    {
                        Name = "Unknown",
                        Resources = new Dictionary<ResourceId, int>()
                        {
                            {ResourceId.Energy, 25},
                            {ResourceId.Gold, 300},
                            {ResourceId.Gems, 10}
                        }
                    }
                },
                SelectedPlayerIndex = 0
            };
        }
    }
}