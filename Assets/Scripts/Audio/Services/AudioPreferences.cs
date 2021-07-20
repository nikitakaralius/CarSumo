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

        public SoundSettings Sound
            => new SoundSettings { MusicVolume = ServiceSound.MusicVolume, SfxVolume = ServiceSound.SfxVolume};
        private SoundSettings ServiceSound => _settingsService.StoredData.Sound;
        
        public async void Init()
        {
            var operationHandle = Addressables.LoadAssetAsync<AudioMixer>(AudioMixerAsset);

            await operationHandle.Task;

            _audioMixer = operationHandle.Result;

            _audioMixer.SetFloat(MusicVolume, MapVolume(ServiceSound.MusicVolume));
            _audioMixer.SetFloat(SfxVolume, MapVolume(ServiceSound.SfxVolume));
        }

        public void ChangeMusicVolume(float volume)
        {
            ChangeVolume(MusicVolume, volume, ref ServiceSound.MusicVolume);            
        }

        public void ChangeSfxVolume(float volume)
        {
            ChangeVolume(SfxVolume, volume, ref ServiceSound.SfxVolume);
        }

        private void ChangeVolume(string exposedParameter, float volume, ref float volumeData)
        {
            _audioMixer.SetFloat(exposedParameter, MapVolume(volume));
            volumeData = volume;
            _settingsService.Save();
        }

        private float MapVolume(float volume)
        {
            return Map.MapByRanges(volume, 0, 1, -80, 0);
        }
    }
}