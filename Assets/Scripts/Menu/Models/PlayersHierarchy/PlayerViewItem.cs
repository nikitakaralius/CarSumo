using CarSumo.Players.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.Menu.Models
{
    public class PlayerViewItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private SelectPlayerHighlight _highlight;

        private IPlayerViewSelect _playerSelect;

        public SelectPlayerHighlight Highlight => _highlight;
        public PlayerProfile Profile { get; private set; }

        public void Init(PlayerProfile profile, IPlayerViewSelect playerSelect)
        {
            Profile = profile;
            _playerSelect = playerSelect;
            _icon.sprite = profile.Icon;
            _name.text = profile.Name;
        }

        public void MakeSelected()
        {
            _playerSelect.MakePlayerSelected(this);
        }
    }
}