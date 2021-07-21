using System.Collections.Generic;
using DataManagement.Players.Services;
using DataManagement.Resources.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace CarSumo.Menu.Models.Resources
{
    public class MenuResource : MonoBehaviour
    {
        [SerializeField] private ResourceId _resource;
        [SerializeField] private TMP_Text _quantity;
        
        private PlayersDataService _playersDataService;

        [Inject]
        private void Construct(PlayersDataService playersDataService)
        {
            _playersDataService = playersDataService;
        }

        private Dictionary<ResourceId, int> Resources => _playersDataService.StoredData.SelectedPlayer.Resources;

        private void Start()
        {
            _quantity.text = Resources[_resource].ToString();
        }
    }
}