using CarSumo.Calculations;
using CarSumo.GameSettings.Services;
using CarSumo.GameSettings.Structs;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

namespace CarSumo.Audio.Services
{
    public class AudioPreferences : IAudioPreferences
    {
        private const string MusicVolume = "MusicVolume";
        private const string SfxVolume = "SfxVolume";
        
        private const string AudioMixerAsset = "AudioMixer";
        private readonly SettingsService _settingsService;
        private AudioMixer _audioMixer;

        public AudioPreferences(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        private SoundSettings Sound => _settingsService.StoredData.Sound;

        public async void Init()
        {
            var operationHandle = Addressables.LoadAssetAsync<AudioMixer>(AudioMixerAsset);

            await operationHandle.Task;

            _audioMixer = operationHandle.Result;

            _audioMixer.SetFloat(MusicVolume, MapVolume(Sound.MusicVolume));
            _audioMixer.SetFloat(SfxVolume, MapVolume(Sound.SfxVolume));
        }

        public void ChangeMusicVolume(float volume)
        {
            ChangeVolume(MusicVolume, volume, ref Sound.MusicVolume);            
        }

        public void ChangeSfxVolume(float volume)
        {
            ChangeVolume(SfxVolume, volume, ref Sound.SfxVolume);
        }

        private void ChangeVolume(string exposedParameter, float volume, ref float volumeData)
        {
            _audioMixer.SetFloat(exposedParameter, volume);
            volumeData = volume;
            _settingsService.Save();
        }

        private float MapVolume(float volume)
        {
            return Map.MapByRanges(volume, 0, 1, -80, 0);
        }
    }
}