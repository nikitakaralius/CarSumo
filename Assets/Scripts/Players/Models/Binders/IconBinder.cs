using AdvancedAudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.Players.Models.Binders
{
    [RequireComponent(typeof(Icon))]
    [RequireComponent(typeof(Button))]
    public class IconBinder : MonoBehaviour
    {
        private Icon _icon;
        private Button _button;

        private void Awake()
        {
            _icon = GetComponent<Icon>();
            _button = GetComponent<Button>();
        }

        public void Bind(Icon userIcon, IAudioPlayer clickSoundPlayer)
        {
            _button.onClick.AddListener(() =>
            {
                clickSoundPlayer.Play();
                _icon.DrawOn(userIcon);
            });
        }
    }
}