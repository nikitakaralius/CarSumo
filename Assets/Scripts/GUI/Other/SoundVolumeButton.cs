using CarSumo.Audio.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarSumo.GUI.Other
{
    [RequireComponent(typeof(Image))]
    public abstract class SoundVolumeButton : MonoBehaviour
    {
        private const float EnabledVolume = 1.0f;
        private const float DisabledVolume = 0.0f;
        
        [SerializeField] private Sprite _enabled;
        [SerializeField] private Sprite _disabled;

        private Image _image;

        protected IAudioPreferences AudioPreferences { get; private set; }

        [Inject]
        private void Construct(IAudioPreferences preferences)
        {
            AudioPreferences = preferences;
        }

        protected abstract float Volume { get; }

        private void Start()
        {
            _image = GetComponent<Image>();
            ChangeImageByVolume(Volume);
        }

        public void OnButtonClicked()
        {
            ChangeVolume(Volume == EnabledVolume ? DisabledVolume : EnabledVolume);
            ChangeImageByVolume(Volume);
        }

        protected abstract void ChangeVolume(float volume);

        private void ChangeImageByVolume(float volume)
        {
            _image.sprite = volume == DisabledVolume ? _disabled : _enabled;
        }
    }
}