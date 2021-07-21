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

        public void AssignFrom(PlayerProfile profile)
        {
            _icon.sprite = profile.Icon;
            _name.text = profile.Name;
        }
    }
}