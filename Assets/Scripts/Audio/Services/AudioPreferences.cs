using CarSumo.Calculations;
using CarSumo.GameSettings.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using Zenject;

namespace CarSumo.Audio.Services
{
    public class AudioPreferences : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioMixerGroup _sfxGroup;
        
        private const string Volume = "Volume";
        private SettingsService _settings;

        [Inject]
        private void Construct(SettingsService settingsService)
        {
            _settings = settingsService;
            
            _musicGroup.audioMixer.SetFloat("MusicVolume", MapVolume(_settings.StoredData.Sound.MusicVolume));
            _sfxGroup.audioMixer.SetFloat("SfxVolume", MapVolume(_settings.StoredData.Sound.SfxVolume));

        }
        
        public void ChangeMusicVolume(float volume)
        {
            _musicGroup.audioMixer.SetFloat("MusicVolume", MapVolume(volume));
            _settings.StoredData.Sound.MusicVolume = volume;
            _settings.Save();
        }
        
        public void ChangeSfxVolume(float volume)
        {
            _musicGroup.audioMixer.SetFloat("SfxVolume", MapVolume(volume));
            _settings.StoredData.Sound.SfxVolume = volume;
            _settings.Save();
        }

        private float MapVolume(float volume)
        {
            return Map.MapByRanges(volume, 0, 1, -80, 0);
        }
    }
}