using CarSumo.DataModel.Settings;
using UniRx;
using UnityEngine.Audio;

namespace DataModel.GameData.Settings
{
    public class GameAudioSettings : IAudioSettings, IAudioSettingsOperations, IAudioSettingsStatus
    {
        private readonly AudioMixer _audioMixer;
        private readonly IAudioConfiguration _configuration;

        private readonly ReactiveProperty<float> _musicVolume;
        private readonly ReactiveProperty<float> _sfxVolume;
        
        private readonly ReactiveProperty<bool> _musicEnabled;
        private readonly ReactiveProperty<bool> _sfxEnabled;
        
        public const float Disabled = -80.0f;
        public const float Enabled = 0.0f;

        public GameAudioSettings(IAudioConfiguration configuration, float musicVolume, float sfxVolume)
        {
            _audioMixer = configuration.AudioMixer;
            _configuration = configuration;
            _musicVolume = new ReactiveProperty<float>(musicVolume);
            _sfxVolume = new ReactiveProperty<float>(sfxVolume);

            _musicEnabled = new ReactiveProperty<bool>(musicVolume != Disabled);
            _sfxEnabled = new ReactiveProperty<bool>(sfxVolume != Disabled);
        }

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;

        public IReadOnlyReactiveProperty<float> SfxVolume => _sfxVolume;

        public IReadOnlyReactiveProperty<bool> MusicEnabled => _musicEnabled;

        public IReadOnlyReactiveProperty<bool> SfxEnabled => _sfxEnabled;
        
        public void SetActiveMusic(bool active)
        {
            _musicEnabled.Value = active;
            SetActiveVolume(active, _musicVolume, _configuration.MusicVolumeParameter);
        }

        public void SetActiveSfxVolume(bool active)
        {
            _sfxEnabled.Value = active;
            SetActiveVolume(active, _sfxVolume, _configuration.SfxVolumeParameter);
        }

        private void SetActiveVolume(bool active, IReactiveProperty<float> volume, string mixerParameter)
        {
            volume.Value = GetVolumeLevel(active);
            _audioMixer.SetFloat(mixerParameter, volume.Value);
        }

        private float GetVolumeLevel(bool active)
        {
            return active ? Enabled : Disabled;
        }
    }
}