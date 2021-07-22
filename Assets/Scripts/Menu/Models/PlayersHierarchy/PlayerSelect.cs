using System;
using System.Linq;
using CarSumo.Players.Models;
using DataManagement.Players.Models;
using DataManagement.Players.Services;

namespace CarSumo.Menu.Models
{
    public class PlayerSelect : IPlayerSelect, IPlayerProfilesUpdate
    {
        private readonly PlayersDataService _dataService;        

        public PlayerSelect(PlayersDataService dataService)
        {
            _dataService = dataService;
        }

        public event Action Updated;

        private IPlayersRepository Repository => _dataService.StoredData;

        public void MakePlayerSelected(PlayerProfile newSelected)
        {
            Player repositoryModel = FindPlayerWithSameName(Repository, newSelected);

            if (Repository.TryMakePlayerSelected(repositoryModel) == false)
            {
                throw new InvalidOperationException(nameof(repositoryModel));
            }
            
            _dataService.Save();
            Updated?.Invoke();
        }

        private Player FindPlayerWithSameName(IPlayersRepository repository, PlayerProfile profile)
        {
            return repository.Players
                .First(player => player.Name == profile.Name);
        }
    }
}