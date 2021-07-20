using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Players.Models;
using DataManagement.Players.Models;
using UnityEngine;
using Zenject;

namespace CarSumo.Players
{
    public class GamePlayersRepository : MonoBehaviour
    {
        private IPlayersRepository _repository;
        private IPlayerProfileBuilder _profileBuilder;

        private List<PlayerProfile> _playerProfiles;

        [Inject]
        private void Construct(IPlayersRepository playersRepository, IPlayerProfileBuilder profileBuilder)
        {
            _repository = playersRepository;
            _profileBuilder = profileBuilder;
        }

        public event Action SelectedPlayerProfileChanged;

        public IReadOnlyList<PlayerProfile> PlayerProfiles => _playerProfiles;
        public PlayerProfile SelectedPlayerProfile { get; private set; }

        private void Awake()
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

            Player newSelectedPlayer = _repository.Players[index];
            
            if (_repository.TryMakePlayerSelected(newSelectedPlayer) == false)
                return false;

            SelectedPlayerProfile = playerProfile;
            SelectedPlayerProfileChanged?.Invoke();

            return true;
        }

        private List<PlayerProfile> BuildPlayerProfiles()
        {
            return _repository.Players
                .Select(repositoryPlayer => _profileBuilder.BuildFrom(repositoryPlayer))
                .ToList();
        }

        private PlayerProfile BuildSelectedPlayerProfile()
        {
            return _profileBuilder.BuildFrom(_repository.SelectedPlayer);
        }
    }
}