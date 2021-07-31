﻿using DataModel.FileData;
using DataModel.Settings;
using UniRx;
using UnityEngine.Audio;
using Zenject;

namespace DataModel.GameData.Settings
{
    public class GameAudioSettings : IAudioSettings, IAudioSettingsOperations, IInitializable
    {
        private readonly AudioMixer _audioMixer;
        private readonly IAudioConfig _config;
        private readonly IFileService _fileService;

        private ReactiveProperty<float> _musicVolume;
        private ReactiveProperty<float> _sfxVolume;
        
        private const float Disabled = -80.0f;
        private const float Enabled = 0.0f;

        public GameAudioSettings(AudioMixer audioMixer, IAudioConfig config, IFileService fileService)
        {
            _audioMixer = audioMixer;
            _config = config;
            _fileService = fileService;
        }

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;

        public IReadOnlyReactiveProperty<float> SfxVolume => _sfxVolume;

        public void Initialize()
        {
            var settings = _fileService.Load<SerializableAudioSettings>(_config.FilePath);
            _musicVolume = new ReactiveProperty<float>(settings.MusicVolume);
            _sfxVolume = new ReactiveProperty<float>(settings.SfxVolume);
        }

        public void SetActiveMusic(bool active)
        {
            SetActiveVolume(active, _musicVolume, _config.MusicVolumeParameter);
        }

        public void SetActiveSfxVolume(bool active)
        { 
            SetActiveVolume(active, _sfxVolume, _config.SfxVolumeParameter);
        }
        private void SetActiveVolume(bool active, IReactiveProperty<float> volume, string mixerParameter)
        {
            volume.Value = GetVolumeLevel(active);
            _audioMixer.SetFloat(mixerParameter, volume.Value);
            _fileService.Save(ToSerializableSettings(this), _config.FilePath);
        }

        private float GetVolumeLevel(bool active)
        {
            return active ? Enabled : Disabled;
        }

        private SerializableAudioSettings ToSerializableSettings(IAudioSettings settings)
        {
            return new SerializableAudioSettings()
            {
                MusicVolume = settings.MusicVolume.Value,
                SfxVolume = settings.SfxVolume.Value
            };
        }
    }
}