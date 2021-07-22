using System;
using System.Linq;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
using UnityEngine;
using Zenject;

namespace CarSumo.Menu.Models
{
    public class PlayerSelect : IPlayerSelect
    {
        private readonly PlayersDataService _dataService;
        private PlayerViewItem _selected = null;

        public PlayerSelect(PlayersDataService dataService)
        {
            _dataService = dataService;
        }

        private IPlayersRepository Repository => _dataService.StoredData;
        
        public void MakePlayerSelected(PlayerViewItem newSelected)
        {
            Player repositoryModel = FindPlayerWithSameName(Repository, newSelected);

            if (Repository.TryMakePlayerSelected(repositoryModel) == false)
            {
                throw new InvalidOperationException(nameof(repositoryModel));
            }
            
            _dataService.Save();

            if (_selected != null)
            {
                _selected.Highlight.MakeRegular();
            }
            
            newSelected.Highlight.MakeSelected();
            _selected = newSelected;
        }

        private Player FindPlayerWithSameName(IPlayersRepository repository, PlayerViewItem viewItem)
        {
            return repository.Players
                .First(player => player.Name == viewItem.Profile.Name);
        }
    }
}