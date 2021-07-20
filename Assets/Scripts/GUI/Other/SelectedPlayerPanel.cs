using CarSumo.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Other
{
    public class SelectedPlayerPanel : MonoBehaviour
    {
        [SerializeField] private GamePlayersRepository _repository;

        [Header("Components")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        private void OnEnable()
        {
            _repository.SelectedPlayerProfileChanged += OnSelectedPlayerProfileChanged;
        }

        private void OnDisable()
        {
            _repository.SelectedPlayerProfileChanged -= OnSelectedPlayerProfileChanged;
        }

        private void OnSelectedPlayerProfileChanged()
        {
            _name.text = _repository.SelectedPlayerProfile.Name;
            _icon.sprite = _repository.SelectedPlayerProfile.Icon;
        }
    }
}