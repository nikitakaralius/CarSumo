using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Menu.Models;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
using DataManagement.Resources;
using TMPro;
using UnityEngine;
using Zenject;

namespace CarSumo.Players.Models
{
    public class PlayerProfileCreator : MonoBehaviour
    {
        [SerializeField] private Icon _selectedIcon;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private GameObject _newPlayerWindow;
        [SerializeField] private PlayersHierarchy _hierarchy;

        private PlayersDataService _dataService;
        
        private Dictionary<ResourceId, int> InitialResources =>
            new Dictionary<ResourceId, int>
            {
                {ResourceId.Energy, 50},
                {ResourceId.Gems, 5},
                {ResourceId.Gold, 250}
            };

        [Inject]
        private void Construct(PlayersDataService dataService)
        {
            _dataService = dataService;
        }

        public async void CreateNewProfile()
        {
            object iconKey = _selectedIcon.Key;
            string name = _nameField.text;

            var newPlayer = new Player()
            {
                Name = name,
                Icon = iconKey,
                Resources = InitialResources
            };

            if (_dataService.StoredData.TryAddPlayer(newPlayer) == false)
            {
                throw new InvalidOperationException($"Unable to create player with name {name}");
            }

            _dataService.Save();

            await _hierarchy.UpdateProfiles();
            _newPlayerWindow.SetActive(false);
        }
    }
}