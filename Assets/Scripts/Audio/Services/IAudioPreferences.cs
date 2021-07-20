using CarSumo.GameSettings.Structs;

namespace CarSumo.Audio.Services
{
    public interface IAudioPreferences
    {
        SoundSettings Sound { get; }
        
        void Init();
        void ChangeMusicVolume(float volume);
        void ChangeSfxVolume(float volume);
    }
}