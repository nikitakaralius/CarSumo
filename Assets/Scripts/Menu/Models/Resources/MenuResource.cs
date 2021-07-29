using System.Collections.Generic;
using DataManagement.Resources;
using TMPro;
using UnityEngine;
using Zenject;

namespace CarSumo.Menu.Models.Resources
{
    public class MenuResource : MonoBehaviour
    {
        [SerializeField] private ResourceId _resource;
        [SerializeField] private TMP_Text _quantity;
        
        private IPlayerProfilesProvider _profilesProvider;
        private IPlayerProfilesUpdate _update;

        [Inject]
        private void Construct(IPlayerProfilesProvider profilesProvider, IPlayerProfilesUpdate update)
        {
            _profilesProvider = profilesProvider;
            _update = update;
        }

        private IReadOnlyDictionary<ResourceId, int> Resources => _profilesProvider.SelectedPlayer.Resources;

        private void OnEnable()
        {
            _update.Updated += UpdateResources;
        }

        private void Start()
        {
            UpdateResources();
        }

        private void OnDisable()
        {
            _update.Updated -= UpdateResources;
        }

        private void UpdateResources()
        {
            _quantity.text = Resources[_resource].ToString();
        }
    }
}