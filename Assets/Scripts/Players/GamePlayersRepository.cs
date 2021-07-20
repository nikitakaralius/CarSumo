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

        [Inject]
        private void Construct(IPlayersRepository playersRepository, IPlayerProfileBuilder profileBuilder)
        {
            _repository = playersRepository;
            _profileBuilder = profileBuilder;
        }
        
        public IReadOnlyList<PlayerProfile> PlayerProfiles { get; private set; }
        public PlayerProfile SelectedPlayerProfile { get; private set; }

        private void Awake()
        {
            PlayerProfiles = BuildPlayerProfiles();
            SelectedPlayerProfile = BuildSelectedPlayerProfile();
        }

        private IReadOnlyList<PlayerProfile> BuildPlayerProfiles()
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