using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Players.Models;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
using UnityEngine;
using Zenject;

namespace CarSumo.Players
{
    public class GamePlayersRepository : MonoBehaviour
    {
        private PlayersDataService _playerDataService;
        private IPlayerProfileBinder _profileBinder;

        private List<PlayerProfile> _playerProfiles;

        [Inject]
        private void Construct(PlayersDataService playersRepository, IPlayerProfileBinder profileBinder)
        {
            _playerDataService = playersRepository;
            _profileBinder = profileBinder;
        }

        public event Action SelectedPlayerProfileChanged;

        public IReadOnlyList<PlayerProfile> PlayerProfiles => _playerProfiles;
        public PlayerProfile SelectedPlayerProfile { get; private set; }

        private IPlayersRepository PlayersRepository => _playerDataService.StoredData;

        private void Start()
        {
            _playerProfiles = BuildPlayerProfiles();
            SelectedPlayerProfile = BuildSelectedPlayerProfile();
            SelectedPlayerProfileChanged?.Invoke();
        }

        public bool TryChangeSelectedPlayerProfile(PlayerProfile playerProfile)
        {
            int index = _playerProfiles.IndexOf(playerProfile);

            if (index == -1)
                return false;

            Player newSelectedPlayer = PlayersRepository.Players[index];

            if (PlayersRepository.TryMakePlayerSelected(newSelectedPlayer) == false)
                return false;

            SelectedPlayerProfile = playerProfile;
            _playerDataService.Save();
            SelectedPlayerProfileChanged?.Invoke();

            return true;
        }

        private List<PlayerProfile> BuildPlayerProfiles()
        {
            return PlayersRepository.Players
                .Select(repositoryPlayer => _profileBinder.BindFrom(repositoryPlayer))
                .ToList();
        }

        private PlayerProfile BuildSelectedPlayerProfile()
        {
            return _profileBinder.BindFrom(PlayersRepository.SelectedPlayer);
        }
    }
}