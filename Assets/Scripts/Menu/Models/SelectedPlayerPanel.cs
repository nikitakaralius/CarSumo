using System;
using CarSumo.Menu.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarSumo.Menu.Models
{
    public class SelectedPlayerPanel : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        
        private IPlayerProfilesProvider _profilesProvider;

        [Inject]
        private void Construct(IPlayerProfilesProvider provider)
        {
            _profilesProvider = provider;
        }

        private void OnEnable()
        {
            _profilesProvider.Updated += UpdatePlayerProfile;
        }

        private void OnDisable()
        {
            _profilesProvider.Updated -= UpdatePlayerProfile;
        }

        private void UpdatePlayerProfile()
        {
            _name.text = _profilesProvider.SelectedPlayer.Name;
            _icon.sprite = _profilesProvider.SelectedPlayer.Icon;
        }
    }
}