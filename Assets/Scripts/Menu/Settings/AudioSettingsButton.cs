using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using CarSumo.DataModel.Settings;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Settings
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public abstract class AudioSettingsButton : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledImage;
        [SerializeField] private Sprite _disabledImage;

        public abstract IReadOnlyReactiveProperty<bool> Enabled { get; }

        protected IAudioSettingsOperations AudioSettingsOperations { get; private set; }
        protected IAudioSettingsStatus AudioStatus { get; private set; }

        private IAudioPlayer _uiSoundPlayer;
        private Image _image;

        [Inject]
        private void Construct(IAudioSettingsOperations audioSettingsOperations,
                               IAudioSettingsStatus status,
                               IAudioPlayer uiSoundPlayer)
        {
            AudioSettingsOperations = audioSettingsOperations;
            AudioStatus = status;
            _uiSoundPlayer = uiSoundPlayer;
        }

        private void OnEnable()
        {
            _image = GetComponent<Image>();
            Button button = GetComponent<Button>();
            
            button.onClick.AddListener(OnButtonClicked);
            button.onClick.AddListener(PlaySound);
            Enabled.Subscribe(ChangeImage);
        }

        protected abstract void OnButtonClicked();

        private void PlaySound()
        {
            _uiSoundPlayer.Play();
        }
        
        private void ChangeImage(bool enabled)
        {
            _image.sprite = enabled ? _enabledImage : _disabledImage;
        }
    }
}